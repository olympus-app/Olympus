using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class PasskeyRegisterEndpoint : Endpoint.WithRequest<PasskeyRegisterRequest> {

	public UserManager<User> UserManager { get; set; } = default!;

	public SignInManager<User> SignInManager { get; set; } = default!;

	public override void Configure() {

		Post(CoreRoutes.Identity.Passkeys.Register);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override async Task<Void> HandleAsync(PasskeyRegisterRequest input, CancellationToken cancellationToken) {

		var user = await UserManager.GetUserAsync(User);

		if (user is null) return await Send.NotFoundAsync(ErrorsStrings.Values.UserInactiveOrNotFound, cancellationToken);

		if (!string.IsNullOrEmpty(input.Error)) return await Send.BadRequestAsync(input.Error, cancellationToken);

		if (string.IsNullOrEmpty(input.Credential)) return await Send.BadRequestAsync(ErrorsStrings.Values.PasskeyMissingCredential, cancellationToken);

		try {

			var attestationResult = await SignInManager.PerformPasskeyAttestationAsync(input.Credential);

			if (!attestationResult.Succeeded) return await Send.BadRequestAsync(attestationResult.Failure.Message, cancellationToken);

			var saveResult = await UserManager.AddOrUpdatePasskeyAsync(user, attestationResult.Passkey);

			if (!saveResult.Succeeded) return await Send.ErrorAsync(ErrorsStrings.Values.PasskeyRegisterFailed, cancellationToken);

			return await Send.CreatedAsync(cancellationToken);

		} catch (Exception exception) {

			return await Send.ExceptionAsync(exception, cancellationToken);

		}

	}

}
