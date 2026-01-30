using Microsoft.AspNetCore.Antiforgery;

namespace Olympus.Core.Backend.Identity;

public class AntiforgeryEndpoint : Endpoint.WithResponse<AntiforgeryResponse> {

	public IAntiforgery Antiforgery { get; set; } = default!;

	public override void Configure() {

		Get(CoreRoutes.Identity.Antiforgery);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override Task<Void> HandleAsync(CancellationToken cancellationToken) {

		var tokens = Antiforgery.GetAndStoreTokens(HttpContext);

		var response = new AntiforgeryResponse {
			Header = tokens.HeaderName,
			Token = tokens.RequestToken,
		};

		return Send.SuccessAsync(response, cancellationToken);

	}

}
