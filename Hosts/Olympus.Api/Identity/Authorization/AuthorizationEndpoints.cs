namespace Olympus.Api.Identity;

public static class AuthorizationEndpoints {

	private const string TagName = "Authorization";

	public static void MapAuthorizationEndpoints(this WebApplication app) {

		app.MapGet(IdentitySettings.Endpoints.Antiforgery, AuthorizationService.AntiforgeryToken)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .RequireAuthorization();

	}

}
