namespace Olympus.Core.Backend.Identity;

public class TokenListEndpoint : Endpoint.WithResponse<ListResult<TokenListResponse>> {

	public IDatabaseService Database { get; set; } = default!;

	public IEntityResponseMapper<Token, TokenListResponse> ResponseMapper { get; set; } = default!;

	public override void Configure() {

		Get(CoreRoutes.Identity.Tokens.List);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override async Task<Void> HandleAsync(CancellationToken cancellationToken) {

		var userId = User.Id;

		var tokens = Database.Set<Token>().AsNoTracking().Where(token => token.UserId == userId);

		var response = await ResponseMapper.ProjectFromEntity(tokens).ToListAsync(cancellationToken);

		return await Send.ListAsync(response, Web.ResponseCache.PrivateRevalidate, cancellationToken);

	}

}
