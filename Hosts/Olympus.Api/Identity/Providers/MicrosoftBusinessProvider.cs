using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Olympus.Api.Identity;

public class MicrosoftBusinessProvider(AppSettings settings) : IConfigureNamedOptions<OpenIdConnectOptions> {

	private readonly MicrosoftBusinessSettings Settings = settings.Identity.Providers.MicrosoftBusiness;

	public record MicrosoftBusinessUserModel {

		public string? DisplayName { get; set; }
		public string? JobTitle { get; set; }
		public string? Mail { get; set; }

	}

	public void Configure(OpenIdConnectOptions options) { }

	public void Configure(string? name, OpenIdConnectOptions options) {

		if (name != IdentityProviderType.MicrosoftBusiness.Name) return;

		ArgumentException.ThrowIfNullOrEmpty(Settings.Domain);
		ArgumentException.ThrowIfNullOrEmpty(Settings.TenantId);
		ArgumentException.ThrowIfNullOrEmpty(Settings.ClientId);
		ArgumentException.ThrowIfNullOrEmpty(Settings.ClientSecret);
		ArgumentException.ThrowIfNullOrEmpty(Settings.CallbackPath);

		options.ClientId = Settings.ClientId;
		options.ClientSecret = Settings.ClientSecret;
		options.SignInScheme = IdentityConstants.ExternalScheme;
		options.Authority = $"https://login.microsoftonline.com/{Settings.TenantId}/v2.0";
		options.MetadataAddress = $"https://login.microsoftonline.com/{Settings.TenantId}/v2.0/.well-known/openid-configuration";
		options.CallbackPath = Settings.CallbackPath;
		options.ResponseType = "code";
		options.Scope.Add("User.Read");
		options.Scope.Add("profile");
		options.Scope.Add("openid");
		options.Scope.Add("email");
		options.SaveTokens = true;

		options.TokenValidationParameters = new TokenValidationParameters() {
			NameClaimType = "name",
			ValidateIssuer = true,
		};

		options.Events.OnTicketReceived = HandleTicketReceivedAsync;

	}

	private static async Task HandleTicketReceivedAsync(TicketReceivedContext context) {

		var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
		var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
		var httpClientFactory = context.HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>();

		var principal = context.Principal;
		var name = principal?.FindFirstValue(ClaimTypes.Name) ?? principal?.FindFirstValue("name") ?? AppUsers.Unknown.Name;
		var email = principal?.FindFirstValue(ClaimTypes.Email) ?? principal?.FindFirstValue("email") ?? AppUsers.Unknown.Email;

		if (string.IsNullOrEmpty(email) || principal is null) return;

		var user = await userManager.FindByEmailAsync(email);

		if (user is null) {

			user = new User() {
				Name = name,
				Email = email,
				UserName = email,
				IsActive = true,
			};

			var createResult = await userManager.CreateAsync(user);

			if (!createResult.Succeeded) {

				context.Fail(ErrorsStrings.Values.UserCreationFailed);

				return;

			}

		}

		var accessToken = context.Properties?.GetTokenValue("access_token");

		if (!string.IsNullOrEmpty(accessToken)) {

			try {

				var client = httpClientFactory.CreateClient();

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

				var userInfo = await client.GetFromJsonAsync<MicrosoftBusinessUserModel>("https://graph.microsoft.com/v1.0/me?$select=displayName,jobTitle,mail");

				if (userInfo is not null) {

					user.Name = userInfo.DisplayName ?? AppUsers.Unknown.Name;
					user.Title = userInfo.JobTitle;

				}

				var userPhoto = await client.GetAsync("https://graph.microsoft.com/v1.0/me/photos/240x240/$value");

				// if (userPhoto.IsSuccessStatusCode) user.Photo = await userPhoto.Content.ReadAsByteArrayAsync();

			} catch { } finally {

				await userManager.UpdateAsync(user);

			}

		}

		var loginProvider = context.Scheme.Name;
		var providerKey = principal.FindFirstValue("oid") ?? principal.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier");

		var logins = await userManager.GetLoginsAsync(user);

		if (!logins.Any(login => login.LoginProvider == loginProvider && login.ProviderKey == providerKey)) {

			var info = new UserLoginInfo(loginProvider, providerKey!, context.Scheme.DisplayName);

			await userManager.AddLoginAsync(user, info);

		}

		await signInManager.SignInAsync(user, isPersistent: true);

		await context.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

		context.Response.Redirect("/");

		context.HandleResponse();

	}

}
