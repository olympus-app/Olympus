using System.Net.Http.Json;

namespace Olympus.Core.Frontend.Services;

public sealed class ApiClient(HttpClient client) {

	public Task<TResponse?> GetAsync<TResponse>(string route) where TResponse : class, IResponse {

		return client.GetFromJsonAsync<TResponse>(route);

	}

	public async Task PostAsync<TRequest>(string route, TRequest request) where TRequest : class, IRequest {

		using var response = await client.PostAsJsonAsync(route, request);

		response.EnsureSuccessStatusCode();

	}

	public async Task<TResponse?> PostAsync<TRequest, TResponse>(string route, TRequest request) where TRequest : class, IRequest where TResponse : class, IResponse {

		using var response = await client.PostAsJsonAsync(route, request);

		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<TResponse>();

	}

	public async Task PutAsync<TRequest>(string route, TRequest request) where TRequest : class, IRequest {

		using var response = await client.PutAsJsonAsync(route, request);

		response.EnsureSuccessStatusCode();

	}

	public async Task<TResponse?> PutAsync<TRequest, TResponse>(string route, TRequest request) where TRequest : class, IRequest where TResponse : class, IResponse {

		using var response = await client.PutAsJsonAsync(route, request);

		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<TResponse>();

	}

	public async Task PatchAsync<TRequest>(string route, TRequest request) where TRequest : class, IRequest {

		using var response = await client.PatchAsJsonAsync(route, request);

		response.EnsureSuccessStatusCode();

	}

	public async Task<TResponse?> PatchAsync<TRequest, TResponse>(string route, TRequest request) where TRequest : class, IRequest where TResponse : class, IResponse {

		using var response = await client.PatchAsJsonAsync(route, request);

		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<TResponse>();

	}

	public async Task DeleteAsync(string route) {

		using var response = await client.DeleteAsync(route);

		response.EnsureSuccessStatusCode();

	}

	public async Task<TResponse?> DeleteAsync<TResponse>(string route) where TResponse : class, IResponse {

		using var response = await client.DeleteAsync(route);

		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<TResponse>();

	}

}
