namespace Olympus.Api.Identity;

public static class AuthenticationEndpoints {

	private const string TagName = "Authentication";

	public static void MapAuthenticationEndpoints(this WebApplication app) {

		app.MapPost(IdentitySettings.Endpoints.Login, AuthenticationService.LoginAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Accepts<UserLoginRequest>(ContentTypes.Json)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .DisableAntiforgery()
		   .AllowAnonymous();

		app.MapPost(IdentitySettings.Endpoints.Logout, AuthenticationService.LogoutAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
		   .DisableAntiforgery()
		   .RequireAuthorization();

		app.MapGet(IdentitySettings.Endpoints.Identity, AuthenticationService.Identity)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces<UserIdentityResponse>(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
		   .RequireAuthorization();

		app.MapGet(IdentitySettings.Endpoints.ExternalLogin, AuthenticationService.ExternalLogin)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .AllowAnonymous();

		app.MapGet(IdentitySettings.Endpoints.ExternalCallback, AuthenticationService.ExternalLoginCallback)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .AllowAnonymous();

	}

}
