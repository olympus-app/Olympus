namespace Olympus.Api.Identity;

[Mapper]
public partial class TokenMapper {

	public static partial TokenListResponse Map(Token entity);

	public static partial IQueryable<TokenListResponse> Project(IQueryable<Token> query);

	public static partial Token Create(TokenCreateRequest request);

}
