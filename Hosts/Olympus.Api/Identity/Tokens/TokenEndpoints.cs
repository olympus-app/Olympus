namespace Olympus.Api.Identity;

public static class TokenEndpoints {

	private const string TagName = "Tokens";

	public static void MapTokensEndpoints(this WebApplication app) {

		app.MapGet(TokenSettings.Endpoints.List, TokenService.ListAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces<IEnumerable<TokenListResponse>>(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
		   .RequireAuthorization();

		app.MapPost(TokenSettings.Endpoints.Create, TokenService.CreateAsync)
			.WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
			.RequireAuthorization();

		app.MapDelete(TokenSettings.Endpoints.Delete + "/{key}", TokenService.DeleteAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
		   .RequireAuthorization();

	}

}
