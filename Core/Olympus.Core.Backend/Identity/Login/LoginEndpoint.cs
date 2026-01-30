using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class LoginEndpoint : Endpoint.WithRequest<LoginRequest> {

	public SignInManager<User> SignInManager { get; set; } = default!;

	public override void Configure() {

		Post(CoreRoutes.Identity.Login);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));
		DisableAntiforgery();
		AllowAnonymous();

	}

	public override async Task<Void> HandleAsync(LoginRequest request, CancellationToken cancellationToken) {

		SignInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;

		var result = await SignInManager.PasswordSignInAsync(request.Email, request.Password, request.IsPersistent, true);

		if (!result.Succeeded) return await Send.ErrorAsync(ErrorsStrings.Values.LoginFailed, cancellationToken);

		if (result.IsLockedOut) return await Send.ForbiddenAsync(cancellationToken);

		return await Send.SuccessAsync(cancellationToken);

	}

}
