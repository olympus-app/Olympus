using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class ExternalLoginEndpoint : Endpoint.WithRequest<ExternalLoginRequest> {

	public SignInManager<User> SignInManager { get; set; } = default!;

	public override void Configure() {

		Get(CoreRoutes.Identity.ExternalLogin);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));
		AllowAnonymous();

	}

	public override Task<Void> HandleAsync(ExternalLoginRequest request, CancellationToken cancellationToken) {

		var callbackUrl = AppUriBuilder.FromAuth(CoreRoutes.Identity.ExternalLoginCallback).WithQuery("returnUrl", request.ReturnUrl);

		var properties = SignInManager.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl);

		return Send.ChallengeAsync(properties, [request.Provider]);

	}

}
