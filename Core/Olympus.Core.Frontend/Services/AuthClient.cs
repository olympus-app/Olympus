using System.Net.Http.Json;

namespace Olympus.Core.Frontend.Services;

public sealed class AuthClient(HttpClient client) {

	private readonly HttpClient HttpClient = client;

	public Task<AntiforgeryResponse?> GetAntiforgeryAsync() => HttpClient.GetFromJsonAsync<AntiforgeryResponse>("/auth/antiforgery");

	public Task<UserIdentityResponse?> GetIdentityAsync() => HttpClient.GetFromJsonAsync<UserIdentityResponse>("/auth/identity");

	public Task<HttpResponseMessage> LoginAsync(UserLoginRequest request) => HttpClient.PostAsJsonAsync("/auth/login?useCookies=true", request);

	public Task<HttpResponseMessage> LogoutAsync() => HttpClient.PostAsync("/auth/logout", null);

}
