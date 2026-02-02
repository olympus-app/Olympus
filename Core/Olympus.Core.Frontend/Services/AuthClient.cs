using System.Net.Http.Json;

namespace Olympus.Core.Frontend.Services;

public sealed class AuthClient(HttpClient client) {

	public Task<HttpResponseMessage> LoginAsync(LoginRequest request, bool useCookies = true) {

		var url = AppUriBuilder.FromAuth(CoreRoutes.Identity.Login).WithQuery(useCookies).AsUrl();

		return client.PostAsJsonAsync(url, request);

	}

	public Task<HttpResponseMessage> LogoutAsync() {

		var url = AppUriBuilder.FromAuth(CoreRoutes.Identity.Logout).AsUrl();

		return client.PostAsync(url, null);

	}

	public Task<AntiforgeryResponse?> GetAntiforgeryAsync() {

		var url = AppUriBuilder.FromAuth(CoreRoutes.Identity.Antiforgery).AsUrl();

		return client.GetFromJsonAsync<AntiforgeryResponse>(url);

	}

	public Task<IdentityResponse?> GetIdentityAsync() {

		var url = AppUriBuilder.FromAuth(CoreRoutes.Identity.Info).AsUrl();

		return client.GetFromJsonAsync<IdentityResponse>(url);

	}

}
