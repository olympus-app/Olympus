namespace Olympus.Api.ErrorHandling;

public static class GlobalStatusHandlerRegistrator {

	public static void UseGlobalStatusHandlerMiddleware(this WebApplication app) {

		app.UseStatusCodePages(static context => HandleErrorStatusCodeAsync(context.HttpContext));

	}

	private static Task HandleErrorStatusCodeAsync(HttpContext context) {

		if (context.Response.StatusCode < 400 || context.Response.ContentLength is not null) return Task.CompletedTask;

		var response = new ProblemResult() {
			Status = context.Response.StatusCode,
			Message = ((HttpStatusCode)context.Response.StatusCode).Humanized,
		};

		context.Response.ContentType = ContentTypes.Json;

		return context.Response.WriteAsJsonAsync(response);

	}

}
