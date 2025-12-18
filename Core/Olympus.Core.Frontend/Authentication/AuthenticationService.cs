using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Olympus.Core.Frontend.Authentication;

public class AuthenticationService(AuthClient client, StorageService storage) : AuthenticationStateProvider {

	private const string StorageKey = "UserIdentity";

	private readonly AuthenticationState Anonymous = new(new AppClaimsPrincipal());

	public override async Task<AuthenticationState> GetAuthenticationStateAsync() {

		try {

			var identity = await client.GetIdentityAsync();

			await storage.Local.SetAsync(StorageKey, identity);

			if (identity is not null) return CreateState(identity);

		} catch (HttpRequestException) {

			var identity = await storage.Local.GetAsync<UserIdentityResponse>(StorageKey);

			if (identity is not null) return CreateState(identity);

		}

		return Anonymous;

	}

	private static AuthenticationState CreateState(UserIdentityResponse user) {

		var identity = new ClaimsIdentity(IdentitySettings.CookieName, AppClaimsTypes.Name, AppClaimsTypes.Role);

		identity.AddClaim(AppClaimsTypes.Id, user.Id);
		identity.AddClaim(AppClaimsTypes.Name, user.Name);
		identity.AddClaim(AppClaimsTypes.Email, user.Email);
		identity.AddClaim(AppClaimsTypes.UserName, user.UserName);
		identity.AddClaim(AppClaimsTypes.JobTitle, user.JobTitle);
		identity.AddClaim(AppClaimsTypes.Department, user.Department);
		identity.AddClaim(AppClaimsTypes.OfficeLocation, user.OfficeLocation);
		identity.AddClaim(AppClaimsTypes.Country, user.Country);
		identity.AddClaim(AppClaimsTypes.Permissions, user.Permissions);
		identity.AddClaims(AppClaimsTypes.Role, user.Roles);

		var principal = new AppClaimsPrincipal(identity);

		return new AuthenticationState(principal);

	}

	public async Task NotifyLoginSuccessAsync() {

		var state = await GetAuthenticationStateAsync();

		var result = Task.FromResult(state);

		NotifyAuthenticationStateChanged(result);

	}

	public async Task LogoutAsync() {

		await client.LogoutAsync();

		await storage.Local.RemoveAsync(StorageKey);

		var result = Task.FromResult(Anonymous);

		NotifyAuthenticationStateChanged(result);

	}

}
