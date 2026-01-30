namespace Olympus.Api.Identity;

public class ClaimsPrincipalMiddleware(RequestDelegate next) {

	public Task InvokeAsync(HttpContext context) {

		if (context.User.IsAuthenticated) {

			context.User = new AppClaimsPrincipal(context.User);

		}

		return next(context);

	}

}
