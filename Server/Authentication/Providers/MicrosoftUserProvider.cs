using System.Security.Claims;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Olympus.Server.Database;
using Olympus.Server.Statics;

namespace Olympus.Server.Authentication;

public class MicrosoftUserProvider(ITokenAcquisition tokenAcquisition, IHttpClientFactory httpClientFactory) : IUserProvider {

	private readonly ITokenAcquisition _tokenAcquisition = tokenAcquisition;
	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
	private static readonly string[] PROVIDER_ISSUERS = ["https://sts.windows.net/", "https://login.microsoftonline.com/"];
	private const string PROVIDER_NAME = "Microsoft User Provider";
	private const int FORCE_UPDATE_HOURS = 24;

	public bool CanHandle(ClaimsPrincipal principal) {

		var issuer = principal.FindFirstValue("iss");
		if (string.IsNullOrEmpty(issuer)) return false;

		return PROVIDER_ISSUERS.Any(issuer.StartsWith);

	}

	public async Task<User> ProvisionUserAsync(ClaimsPrincipal principal, DefaultDatabaseContext dbContext) {

		var (name, upn, oid) = ExtractClaimsFromPrincipal(principal);
		ValidateRequiredClaims(name, upn, oid);

		var identity = await FindUserIdentityByObjectIdAsync(dbContext, oid);
		if (identity != null) return await HandleExistingIdentityAsync(identity, upn, name);

		var existingUser = await FindExistingUserByUpnAsync(dbContext, upn);
		if (existingUser != null) return HandleExistingUserWithoutIdentity(dbContext, existingUser, upn, oid);

		return await CreateNewUserWithIdentityAsync(dbContext, upn, name, oid);

	}

	private static async Task<UserIdentity?> FindUserIdentityByObjectIdAsync(DefaultDatabaseContext dbContext, string oid) {

		return await dbContext.UserIdentities.Include(i => i.User).Include(i => i.CreatedUser)
			.FirstOrDefaultAsync(i => i.ProviderName == PROVIDER_NAME && i.ProviderKey == oid);

	}

	private static async Task<User?> FindExistingUserByUpnAsync(DefaultDatabaseContext dbContext, string upn) {

		return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == upn);

	}

	private static (string name, string upn, string oid) ExtractClaimsFromPrincipal(ClaimsPrincipal principal) {

		var name = GetFullName(principal);
		var upn = GetClaim(principal, ClaimConstants.Username, ClaimTypes.Upn);
		var oid = GetClaim(principal, ClaimConstants.ObjectId, ClaimConstants.Oid);

		return (name, upn, oid);

	}

	private static void ValidateRequiredClaims(string name, string upn, string oid) {

		if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(upn) || string.IsNullOrWhiteSpace(oid)) {

			throw new InvalidOperationException("Token is missing required claims (name, upn and oid).");

		}

	}

	private static string GetClaim(ClaimsPrincipal principal, params string[] keys) {

		foreach (var key in keys) {

			var value = principal.FindFirstValue(key);
			if (!string.IsNullOrWhiteSpace(value)) return value;

		}

		return string.Empty;

	}

	private static string GetFullName(ClaimsPrincipal principal) {

		var name = principal.FindFirstValue(ClaimConstants.Name);
		if (!string.IsNullOrWhiteSpace(name)) return name;

		var givenName = principal.FindFirstValue(ClaimTypes.GivenName);
		var surname = principal.FindFirstValue(ClaimTypes.Surname);

		if (!string.IsNullOrWhiteSpace(givenName) && !string.IsNullOrWhiteSpace(surname)) {

			return $"{givenName} {surname}";

		}

		return givenName ?? surname ?? string.Empty;

	}

	private async Task<User> HandleExistingIdentityAsync(UserIdentity identity, string upn, string name) {

		await UpdateUserIdentityAsync(identity, upn, name);
		return identity.User;

	}

	private static User HandleExistingUserWithoutIdentity(DefaultDatabaseContext dbContext, User user, string upn, string oid) {

		CreateIdentityForUser(dbContext, user, upn, oid);
		return user;

	}

	private async Task UpdateUserIdentityAsync(UserIdentity identity, string upn, string name) {

		var user = identity.User;
		var userChanged = false;

		if (user.Name != name) {
			user.UpdateName(name);
			userChanged = true;
		}

		if (user.Email != upn) {
			user.UpdateEmail(upn);
			userChanged = true;
		}

		if (identity.ProviderUpn != upn) {
			identity.UpdateUpn(upn);
		}

		var needsGraphUpdate = userChanged || ShouldForceUpdate(user);
		if (needsGraphUpdate) { await UpdateUserFromGraphAsync(user); }

	}

	private static void CreateIdentityForUser(DefaultDatabaseContext dbContext, User user, string upn, string oid) {

		var newIdentity = new UserIdentity(user, PROVIDER_NAME, upn, oid);
		newIdentity.SetCreated(SystemVars.SystemUserID);
		dbContext.UserIdentities.Add(newIdentity);

	}

	private static User CreateUserWithIdentity(DefaultDatabaseContext dbContext, string upn, string name, string oid) {

		var newUser = new User(name, upn);
		var newIdentity = new UserIdentity(newUser, PROVIDER_NAME, upn, oid);

		newUser.SetCreated(SystemVars.SystemUserID);
		newIdentity.SetCreated(SystemVars.SystemUserID);
		dbContext.UserIdentities.Add(newIdentity);

		return newUser;

	}

	private async Task<User> CreateNewUserWithIdentityAsync(DefaultDatabaseContext dbContext, string upn, string name, string oid) {

		var newUser = CreateUserWithIdentity(dbContext, upn, name, oid);
		await UpdateUserFromGraphAsync(newUser);
		return newUser;

	}

	private async Task UpdateUserFromGraphAsync(User user) {

		try {

			var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(["https://graph.microsoft.com/User.Read"]);

			using var httpClient = _httpClientFactory.CreateClient();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/me?$select=displayName,jobTitle,department,officeLocation,country");
			if (!response.IsSuccessStatusCode) return;

			var jsonContent = await response.Content.ReadAsStringAsync();
			using var document = JsonDocument.Parse(jsonContent);
			var root = document.RootElement;

			if (root.TryGetProperty("displayName", out var displayName)) UpdateUserProperty(displayName.GetString(), user.UpdateName);

			if (root.TryGetProperty("jobTitle", out var jobTitle)) UpdateUserProperty(jobTitle.GetString(), user.UpdateJobTitle);

			if (root.TryGetProperty("department", out var department)) UpdateUserProperty(department.GetString(), user.UpdateDepartment);

			if (root.TryGetProperty("officeLocation", out var officeLocation)) UpdateUserProperty(officeLocation.GetString(), user.UpdateOfficeLocation);

			if (root.TryGetProperty("country", out var country)) UpdateUserProperty(country.GetString(), user.UpdateCountry);

			await UpdateUserPhotoAsync(user, httpClient);

			user.SetUpdated(user.ID);

		} catch {

			// Graph API errors should not prevent user provisioning

		}

	}

	private static void UpdateUserProperty(string? newValue, Action<string> updateAction) {

		if (!string.IsNullOrWhiteSpace(newValue)) {

			updateAction(newValue);

		}

	}

	private static async Task UpdateUserPhotoAsync(User user, HttpClient httpClient) {

		try {

			var photoResponse = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/photo/$value");
			if (!photoResponse.IsSuccessStatusCode) return;

			var photoBytes = await photoResponse.Content.ReadAsByteArrayAsync();

			if (user.Photo == null || !user.Photo.SequenceEqual(photoBytes)) {

				user.UpdatePhoto(photoBytes);

			}

		} catch {

			// Photo is optional, ignore errors

		}

	}

	private static bool ShouldForceUpdate(User user) {

		return user.UpdatedAt == null || user.UpdatedAt.Value.AddHours(FORCE_UPDATE_HOURS) < DateTime.UtcNow;

	}

}
