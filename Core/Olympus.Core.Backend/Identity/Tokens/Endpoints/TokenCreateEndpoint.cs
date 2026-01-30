namespace Olympus.Core.Backend.Identity;

public class TokenCreateEndpoint : Endpoint.WithRequest<TokenCreateRequest>.WithResponse<TokenListResponse> {

	public IDatabaseService Database { get; set; } = default!;

	public IEntityRequestMapper<Token, TokenCreateRequest> RequestMapper { get; set; } = default!;

	public IEntityResponseMapper<Token, TokenListResponse> ResponseMapper { get; set; } = default!;

	public override void Configure() {

		Post(CoreRoutes.Identity.Tokens.Create);
		Prefix(AppRoutes.Auth);
		Description(static builder => builder.AutoTagOverride(nameof(AppRoutes.Auth)));

	}

	public override async Task<Void> HandleAsync(TokenCreateRequest request, CancellationToken cancellationToken) {

		var (value, hash) = TokenService.GenerateToken();

		var token = RequestMapper.MapToEntity(request);

		token.Hash = hash;
		token.UserId = User.Id;
		token.Value = string.Create(value.Length, value, static (span, val) => {
			val.AsSpan(0, 3).CopyTo(span);
			span.Slice(3, val.Length - 6).Fill('*');
			val.AsSpan(val.Length - 3, 3).CopyTo(span.Slice(span.Length - 3));
		});

		Database.Add(token);

		await Database.SaveChangesAsync(cancellationToken);

		var response = ResponseMapper.MapFromEntity(token);

		response.Value = value;

		return await Send.CreatedAsync(response, cancellationToken);

	}

}
