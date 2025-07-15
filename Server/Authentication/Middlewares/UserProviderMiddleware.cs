using System.Security.Claims;
using Olympus.Server.Database;
using Olympus.Server.Statics;

namespace Olympus.Server.Authentication;

public class UserProviderMiddleware(RequestDelegate next) {

	private readonly RequestDelegate _next = next;

	public async Task InvokeAsync(HttpContext httpContext, DefaultDatabaseContext dbContext, IEnumerable<IUserProvider> providers) {

		if (httpContext.User.Identity?.IsAuthenticated != true) {

			await _next(httpContext);
			return;

		}

		var provider = providers.FirstOrDefault(p => p.CanHandle(httpContext.User));

		if (provider is null) {

			await RespondWithError(httpContext, StatusCodes.Status403Forbidden, "Identity provider not supported.");
			return;

		}

		var user = await provider.ProvisionUserAsync(httpContext.User, dbContext);

		if (user is null) {

			await RespondWithError(httpContext, StatusCodes.Status403Forbidden, "Unable to provision user for the specified identity.");
			return;

		}

		if (httpContext.User.Identity is not ClaimsIdentity identity) {

			await RespondWithError(httpContext, StatusCodes.Status500InternalServerError, "Identity is not a valid ClaimsIdentity.");
			return;

		}

		if (!identity.HasClaim(c => c.Type == SystemVars.SystemClaim)) {

			identity.AddClaim(new Claim(SystemVars.SystemClaim, user.ID.ToString()));

		}

		if (dbContext.ChangeTracker.HasChanges()) {

			await dbContext.SaveChangesAsync();

		}

		await _next(httpContext);

	}

	private static async Task RespondWithError(HttpContext context, int statusCode, string message) {

		context.Response.StatusCode = statusCode;
		await context.Response.WriteAsync(message);

	}

}
