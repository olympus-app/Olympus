using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class PasskeyListEndpoint : Endpoint.WithResponse<ListResult<PasskeyListResponse>> {

	public UserManager<User> UserManager { get; set; } = default!;

	public override void Configure() {

		Get(CoreRoutes.Identity.Passkeys.List);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override async Task<Void> HandleAsync(CancellationToken cancellationToken) {

		var user = await UserManager.GetUserAsync(User);

		if (user is null) return await Send.UnauthorizedAsync(cancellationToken);

		var passkeys = await UserManager.GetPasskeysAsync(user);

		var response = new ListResult<PasskeyListResponse>(
			passkeys.Select(passkey => new PasskeyListResponse() {
				Id = Base64.ToBase64Url(passkey.CredentialId),
				Name = passkey.Name ?? "Passkey",
			}
		));

		return await Send.SuccessAsync(response, cancellationToken);

	}

}
