namespace Olympus.Api.Identity;

public class ClaimsPrincipalMiddleware(RequestDelegate next) {

	public Task InvokeAsync(HttpContext context) {

		if (context.User.Identity?.IsAuthenticated == true) {

			context.User = new AppClaimsPrincipal(context.User);

		}

		return next(context);

	}

}
