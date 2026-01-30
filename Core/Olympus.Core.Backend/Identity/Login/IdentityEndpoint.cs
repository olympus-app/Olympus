namespace Olympus.Core.Backend.Identity;

public class IdentityEndpoint : Endpoint.WithResponse<IdentityResponse> {

	public override void Configure() {

		Get(CoreRoutes.Identity.Info);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override Task<Void> HandleAsync(CancellationToken cancellationToken) {

		var response = new IdentityResponse {
			Id = User.GetValue<Guid>(AppClaimsTypes.Id) ?? AppUsers.Unknown.Id,
			Name = User.GetValue(AppClaimsTypes.Name) ?? User.Identity?.Name ?? AppUsers.Unknown.Name,
			UserName = User.GetValue(AppClaimsTypes.UserName),
			Email = User.GetValue(AppClaimsTypes.Email) ?? AppUsers.Unknown.Email,
			Title = User.GetValue(AppClaimsTypes.Title),
			Photo = User.GetValue(AppClaimsTypes.Photo),
			Roles = User.GetClaims(AppClaimsTypes.Role).Select(claim => claim.Value).ToList(),
			Permissions = User.GetValue(AppClaimsTypes.Permissions),
		};

		return Send.SuccessAsync(response, cancellationToken);

	}

}
