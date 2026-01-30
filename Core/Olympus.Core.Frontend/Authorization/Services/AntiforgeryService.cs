namespace Olympus.Core.Frontend.Authorization;

public class AntiforgeryService(AuthClient client, SessionStorageService storage) {

	private const string StorageKey = "Antiforgery";

	public async Task<string?> GetAsync() {

		var token = await storage.GetAsync<string>(StorageKey);

		if (!string.IsNullOrEmpty(token)) return token;

		return await RefreshAsync();

	}

	public async Task<string?> RefreshAsync() {

		var response = await client.GetAntiforgeryAsync();

		if (response?.Token is null) return null;

		await storage.SetAsync(StorageKey, response.Token);

		return response.Token;

	}

	public async Task ClearAsync() {

		await storage.RemoveAsync(StorageKey);

	}

}
