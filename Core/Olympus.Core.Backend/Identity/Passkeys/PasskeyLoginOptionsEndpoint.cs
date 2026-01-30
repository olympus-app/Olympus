using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class PasskeyLoginOptionsEndpoint : Endpoint.WithRequest<PasskeyLoginOptionsRequest> {

	public UserManager<User> UserManager { get; set; } = default!;

	public SignInManager<User> SignInManager { get; set; } = default!;

	public override void Configure() {

		Post(CoreRoutes.Identity.Passkeys.LoginOptions);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));
		DisableAntiforgery();
		AllowAnonymous();

	}

	public override async Task<Void> HandleAsync(PasskeyLoginOptionsRequest request, CancellationToken cancellationToken) {

		User? user = null;

		if (!string.IsNullOrEmpty(request.Username)) user = await UserManager.FindByNameAsync(request.Username);

		try {

			var optionsJson = await SignInManager.MakePasskeyRequestOptionsAsync(user);

			return await Send.StringAsync(optionsJson, ContentTypes.Json, cancellationToken);

		} catch (Exception exception) {

			return await Send.ExceptionAsync(exception, cancellationToken);

		}

	}

}
