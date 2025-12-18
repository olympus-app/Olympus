using Microsoft.AspNetCore.Antiforgery;

namespace Olympus.Api.Identity;

public class AntiforgeryMiddleware(RequestDelegate next, IAntiforgery antiforgery) {

	public async Task InvokeAsync(HttpContext context) {

		var endpoint = context.GetEndpoint();

		if (endpoint is not null) {

			var metadata = endpoint.Metadata.GetMetadata<IAntiforgeryMetadata>();

			if (metadata?.RequiresValidation == false) {

				await next(context);

				return;

			}

		}

		if (Verbs.UnsafeVerbs.Contains(context.Request.Method)) {

			try {

				await antiforgery.ValidateRequestAsync(context);

			} catch (AntiforgeryValidationException) {

				context.Response.StatusCode = 400;

				await context.Response.WriteAsJsonAsync(new ProblemResult() {
					Status = HttpStatusCode.BadRequest.Value,
					Message = HttpStatusCode.BadRequest.Humanized,
					Details = "Anti-forgery validation failed.",
				});

				return;

			}

		}

		await next(context);

	}

}
