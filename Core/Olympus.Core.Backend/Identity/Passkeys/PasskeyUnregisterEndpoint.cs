using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class PasskeyUnregisterEndpoint : Endpoint.WithRequest<PasskeyUnregisterRequest> {

	public UserManager<User> UserManager { get; set; } = default!;

	public override void Configure() {

		Delete(CoreRoutes.Identity.Passkeys.Unregister);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override async Task<Void> HandleAsync(PasskeyUnregisterRequest request, CancellationToken cancellationToken) {

		var user = await UserManager.GetUserAsync(User);

		if (user is null) return await Send.NotFoundAsync(ErrorsStrings.Values.UserInactiveOrNotFound, cancellationToken);

		var credentialId = Base64.BytesFromBase64Url(request.Key);

		var result = await UserManager.RemovePasskeyAsync(user, credentialId);

		if (!result.Succeeded) return await Send.ErrorAsync(ErrorsStrings.Values.PasskeyUnregisterFailed, cancellationToken);

		return await Send.SuccessAsync(cancellationToken);

	}

}
