using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class PasskeyLoginEndpoint : Endpoint {

	public SignInManager<User> SignInManager { get; set; } = default!;

	public override void Configure() {

		Post(CoreRoutes.Identity.Passkeys.Login);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));
		DisableAntiforgery();
		AllowAnonymous();

	}

	public override async Task<Void> HandleAsync(CancellationToken cancellationToken) {

		try {

			var jsonElement = await HttpContext.Request.ReadFromJsonAsync<JsonElement>(cancellationToken);

			if (jsonElement.ValueKind == JsonValueKind.Undefined) return await Send.BadRequestAsync(ErrorsStrings.Values.PasskeyMissingCredential, cancellationToken);

			var result = await SignInManager.PasskeySignInAsync(jsonElement.ToString());

			if (!result.Succeeded) return await Send.ErrorAsync(ErrorsStrings.Values.LoginFailed, cancellationToken);

			if (result.IsLockedOut) return await Send.ForbiddenAsync(cancellationToken);

			return await Send.SuccessAsync(cancellationToken);

		} catch (JsonException) {

			return await Send.BadRequestAsync(ErrorsStrings.Values.PasskeyInvalidCredential, cancellationToken);

		} catch (Exception exception) {

			return await Send.ExceptionAsync(exception, cancellationToken);

		}

	}

}
