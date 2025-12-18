using System.Text.Json;
using Microsoft.JSInterop;

namespace Olympus.Core.Frontend.Services;

public class StorageService(LocalStorageService local, SessionStorageService session) {

	public BaseStorageService Local => local;

	public BaseStorageService Session => session;

}

public abstract class BaseStorageService(IJSRuntime js, StorageLocation location) {

	public async Task<T?> GetAsync<T>(string key) {

		var json = await js.InvokeAsync<string?>($"{location.GetLabel()}.getItem", key);

		return json is null ? default : JsonSerializer.Deserialize<T>(json);

	}

	public async Task SetAsync<T>(string key, T value) {

		var json = JsonSerializer.Serialize(value);

		await js.InvokeVoidAsync($"{location.GetLabel()}.setItem", key, json);

	}

	public ValueTask RemoveAsync(string key) {

		return js.InvokeVoidAsync($"{location.GetLabel()}.removeItem", key);

	}

}

public class LocalStorageService(IJSRuntime js) : BaseStorageService(js, StorageLocation.Local);

public class SessionStorageService(IJSRuntime js) : BaseStorageService(js, StorageLocation.Session);
