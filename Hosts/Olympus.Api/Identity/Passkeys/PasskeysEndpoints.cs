namespace Olympus.Api.Identity;

public static class PasskeysEndpoints {

	private const string TagName = "Passkeys";

	public static void MapPasskeysEndpoints(this WebApplication app) {

		app.MapGet(PasskeysSettings.Endpoints.List, PasskeyService.ListAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces<IEnumerable<PasskeyListResponse>>(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
		   .RequireAuthorization();

		app.MapPost(PasskeysSettings.Endpoints.Login, PasskeyService.LoginAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .DisableAntiforgery()
		   .AllowAnonymous();

		app.MapPost(PasskeysSettings.Endpoints.LoginOptions, PasskeyService.LoginOptionsAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .DisableAntiforgery()
		   .AllowAnonymous();

		app.MapPost(PasskeysSettings.Endpoints.Register, PasskeyService.RegisterAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
		   .RequireAuthorization();

		app.MapPost(PasskeysSettings.Endpoints.RegisterOptions, PasskeyService.RegisterOptionsAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
		   .RequireAuthorization();

		app.MapDelete(PasskeysSettings.Endpoints.Unregister + "/{key}", PasskeyService.UnregisterAsync)
		   .WithGroupName(IdentitySettings.GroupName.ToLower()).WithTags(TagName)
		   .Produces(HttpStatusCode.OK.Value)
		   .Produces<ProblemResult>(HttpStatusCode.BadRequest.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Unauthorized.Value)
		   .Produces<ProblemResult>(HttpStatusCode.Forbidden.Value)
		   .RequireAuthorization();

	}

}
