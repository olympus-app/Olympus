using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.JSInterop;

namespace Olympus.Core.Frontend.Authentication;

public class PasskeyService(HttpClient http, IJSRuntime js) {

	// REGISTRO: Updated to send JSON
	public async Task<string?> RegisterPasskeyAsync() {

		// 1. Get Options
		var optionsResponse = await http.PostAsync(AppUriBuilder.FromAuth(CoreRoutes.Identity.Passkeys.RegisterOptions), null);
		if (!optionsResponse.IsSuccessStatusCode) return "Erro ao obter opções do servidor.";

		var optionsJson = await optionsResponse.Content.ReadAsStringAsync();

		// 2. JS Interaction (Returns the full JSON string from navigator.credentials.create)
		var credentialJson = await js.InvokeAsync<string?>("Olympus.passkeys.create", optionsJson);

		if (string.IsNullOrEmpty(credentialJson)) return "Operação cancelada ou falhou no dispositivo.";

		// 3. Send to Backend as JSON
		// We create the model expected by the [FromBody] in the backend
		var request = new PasskeyRegisterRequest() { Credential = credentialJson };

		// Use PostAsJsonAsync instead of MultipartFormDataContent
		var registerResponse = await http.PostAsJsonAsync(AppUriBuilder.FromAuth(CoreRoutes.Identity.Passkeys.Register), request);

		if (registerResponse.IsSuccessStatusCode) return null; // Success

		// Optional: Read specific error message from backend if available
		var errorDetails = await registerResponse.Content.ReadAsStringAsync();

		return !string.IsNullOrWhiteSpace(errorDetails) ? errorDetails : "Falha ao registrar passkey no servidor.";

	}

	// LOGIN: This implementation remains compatible if your backend accepts [FromBody] JsonElement
	public async Task<string?> LoginWithPasskeyAsync(string email) {

		// 1. Get Challenge
		var url = $"{AppUriBuilder.FromAuth(CoreRoutes.Identity.Passkeys.LoginOptions)}?username={Uri.EscapeDataString(email)}";
		var optionsResponse = await http.PostAsync(url, null);

		if (!optionsResponse.IsSuccessStatusCode) return "Não foi possível iniciar o login com passkey.";

		var optionsJson = await optionsResponse.Content.ReadAsStringAsync();

		// 2. JS Interaction
		var credentialJson = await js.InvokeAsync<string?>("Olympus.passkeys.get", optionsJson);

		if (string.IsNullOrEmpty(credentialJson)) return "Login cancelado ou falhou.";

		// 3. Send to Backend
		// Parsing to JsonDocument ensures we send a JSON Object, matching [FromBody] JsonElement on backend
		using var jsonDoc = JsonDocument.Parse(credentialJson);
		var loginResponse = await http.PostAsJsonAsync(AppUriBuilder.FromAuth(CoreRoutes.Identity.Passkeys.Login), jsonDoc);

		if (loginResponse.IsSuccessStatusCode) return null;

		return "Falha na validação da chave de acesso.";

	}

	public ValueTask<bool> IsSupportedAsync() {

		return js.InvokeAsync<bool>("Olympus.passkeys.isSupported");

	}

	public async Task<List<PasskeyListResponse>> GetPasskeysAsync() {

		var passkeys = await http.GetFromJsonAsync<ListResult<PasskeyListResponse>>(AppUriBuilder.FromAuth(CoreRoutes.Identity.Passkeys.List));

		return passkeys?.Items.ToList() ?? [];

	}

	public Task DeletePasskeyAsync(string id) {

		var encodedId = Uri.EscapeDataString(id);

		return http.DeleteAsync($"{AppUriBuilder.FromAuth(CoreRoutes.Identity.Passkeys.Unregister)}?key={encodedId}");

	}

}
