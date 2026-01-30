namespace Olympus.Core.Backend.Identity;

public class TokenDeleteEndpoint : Endpoint.WithRequest<TokenDeleteRequest> {

	public IDatabaseService Database { get; set; } = default!;

	public override void Configure() {

		Delete(CoreRoutes.Identity.Tokens.Delete);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override async Task<Void> HandleAsync(TokenDeleteRequest request, CancellationToken cancellationToken) {

		var userId = User.Id;

		var token = await Database.Set<Token>().FirstOrDefaultAsync(token => token.Id == request.Key && token.UserId == userId, cancellationToken);

		if (token is null) return await Send.NotFoundAsync(cancellationToken);

		Database.Remove(token);

		await Database.SaveChangesAsync(cancellationToken);

		return await Send.DeletedAsync(token, cancellationToken);

	}

}
