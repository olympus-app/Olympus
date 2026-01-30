using Microsoft.AspNetCore.Components;

namespace Olympus.Core.Frontend.Pages;

public partial class Login {

	[Inject]
	public AuthClient Client { get; set; } = default!;

	[Inject]
	public AppAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

	[Inject]
	public NavigationManager Navigation { get; set; } = default!;

	[Inject]
	public PasskeyService PasskeyService { get; set; } = default!;

	[Inject]
	public LocalizationManager LocalizationManager { get; set; } = default!;

	[Parameter]
	[SupplyParameterFromQuery]
	public string? ReturnUrl { get; set; }

	// Objeto vinculado diretamente ao formulário
	private LoginRequest Input { get; set; } = new();

	private bool IsLoading;

	private string? ErrorMessage;

	// Assinatura alterada: Não recebe args, pois o binding é direto no Input
	private async Task OnLoginAsync() {

		IsLoading = true;
		ErrorMessage = null;

		try {

			// Input já contem Email e Password via @bind-Value
			var response = await Client.LoginAsync(Input);

			if (response.IsSuccessStatusCode) {

				var bffProvider = (AppAuthenticationStateProvider)AuthStateProvider;
				await bffProvider.NotifyLoginSuccessAsync();

				var destination = string.IsNullOrWhiteSpace(ReturnUrl) || ReturnUrl.Contains("login", StringComparison.OrdinalIgnoreCase) ? "/" : ReturnUrl;

				Navigation.NavigateTo(destination);

			} else {

				ErrorMessage = "Email ou senha inválidos.";

			}

		} catch (Exception exception) {

			ErrorMessage = $"Erro de conexão: {exception.Message}";

		} finally {

			IsLoading = false;

		}

	}

	private void LoginWithMicrosoft() {

		var provider = "MicrosoftBusiness";
		var destination = string.IsNullOrWhiteSpace(ReturnUrl) ? "/" : ReturnUrl;
		// Importante: Uri.EscapeDataString garante que a URL de retorno seja segura
		var challengeUrl = $"/auth/v1/external-login?provider={provider}&returnUrl={Uri.EscapeDataString(destination)}";

		Navigation.NavigateTo(challengeUrl, forceLoad: true);

	}

	private async Task LoginWithPasskeyAsync() {

		// Passa o email do Input (caso preenchido) para auxiliar na descoberta (autofill)
		var emailToUse = string.IsNullOrEmpty(Input.Email) ? null : Input.Email;

		IsLoading = true;
		ErrorMessage = null;

		try {

			var error = await PasskeyService.LoginWithPasskeyAsync(emailToUse ?? string.Empty);

			if (error is null) {

				var bffProvider = AuthStateProvider;
				await bffProvider.NotifyLoginSuccessAsync();
				Navigation.NavigateTo("/");

			} else {

				ErrorMessage = error;

			}

		} catch (Exception exception) {

			ErrorMessage = $"Erro: {exception.Message}";

		} finally {

			IsLoading = false;

		}

	}

}
