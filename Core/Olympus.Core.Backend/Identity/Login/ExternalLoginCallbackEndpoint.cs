namespace Olympus.Core.Backend.Identity;

public class ExternalLoginCallbackEndpoint : Endpoint.WithRequest<ExternalLoginCallbackRequest> {

	public override void Configure() {

		Get(CoreRoutes.Identity.ExternalLoginCallback);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));
		AllowAnonymous();

	}

	public override async Task<Void> HandleAsync(ExternalLoginCallbackRequest request, CancellationToken cancellationToken) {

		if (request.RemoteError is not null) return await Send.LocalRedirectAsync(AppUriBuilder.FromWeb(CoreRoutes.Identity.Login).WithQuery("error", request.RemoteError));

		if (User.IsAuthenticated) return await Send.LocalRedirectAsync(AppUriBuilder.FromWeb(request.ReturnUrl));

		return await Send.LocalRedirectAsync(AppUriBuilder.FromWeb(CoreRoutes.Identity.Login).WithQuery("error", "LoginFailed"));

	}

}
