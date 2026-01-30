using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Olympus.Core.Frontend.Authentication;

public class AppAuthenticationStateProvider(AuthClient client, StorageService storage) : AuthenticationStateProvider {

	private const string StorageKey = "UserIdentity";

	private Task<AuthenticationState>? AuthenticationStateTask;

	private readonly AuthenticationState Anonymous = new(AppClaimsPrincipal.Anonymous);

	public override Task<AuthenticationState> GetAuthenticationStateAsync() {

		return AuthenticationStateTask ??= LoadAuthenticationStateAsync();

	}

	private async Task<AuthenticationState> LoadAuthenticationStateAsync() {

		var cache = await storage.Local.GetAsync<IdentityResponse>(StorageKey);

		try {

			var identity = await client.GetIdentityAsync();

			if (identity is not null) {

				await storage.Local.SetAsync(StorageKey, identity);

				return CreateState(identity);

			}

		} catch (HttpRequestException exception) {

			if (cache is not null) return CreateState(cache);

			if (exception.StatusCode == HttpStatusCode.Unauthorized) {

				await storage.Local.RemoveAsync(StorageKey);

				return Anonymous;

			}

		} catch (Exception) {

			if (cache is not null) return CreateState(cache);

		}

		return Anonymous;

	}

	private static AuthenticationState CreateState(IdentityResponse user) {

		var identity = new ClaimsIdentity(IdentitySettings.CookieName, AppClaimsTypes.Name, AppClaimsTypes.Role);

		identity.AddClaim(AppClaimsTypes.Id, user.Id);
		identity.AddClaim(AppClaimsTypes.Name, user.Name);
		identity.AddClaim(AppClaimsTypes.UserName, user.UserName);
		identity.AddClaim(AppClaimsTypes.Email, user.Email);
		identity.AddClaim(AppClaimsTypes.Title, user.Title);
		identity.AddClaim(AppClaimsTypes.Permissions, user.Permissions);
		identity.AddClaims(AppClaimsTypes.Role, user.Roles);

		var principal = new AppClaimsPrincipal(identity);

		return new AuthenticationState(principal);

	}

	public async Task NotifyLoginSuccessAsync() {

		AuthenticationStateTask = null;

		var state = await GetAuthenticationStateAsync();

		var result = Task.FromResult(state);

		NotifyAuthenticationStateChanged(result);

	}

	public async Task LogoutAsync() {

		await client.LogoutAsync();

		await storage.Local.RemoveAsync(StorageKey);

		AuthenticationStateTask = null;

		var result = Task.FromResult(Anonymous);

		NotifyAuthenticationStateChanged(result);

	}

}
