using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class LogoutEndpoint : Endpoint {

	public SignInManager<User> SignInManager { get; set; } = default!;

	public override void Configure() {

		Post(CoreRoutes.Identity.Logout);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));
		DisableAntiforgery();

	}

	public override async Task<Void> HandleAsync(CancellationToken cancellationToken) {

		await SignInManager.SignOutAsync();

		return await Send.SuccessAsync(cancellationToken);

	}

}
