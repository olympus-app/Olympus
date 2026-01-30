using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Routing;

namespace Olympus.Api.Identity;

public class AntiforgeryMiddleware(RequestDelegate next, IAntiforgery antiforgery) {

	public async Task InvokeAsync(HttpContext context) {

		var endpoint = context.GetEndpoint();

		if (endpoint is null || endpoint.Metadata.GetMetadata<IHttpMethodMetadata>() is null || endpoint.Metadata.GetMetadata<IgnoreAntiforgeryTokenAttribute>() is not null) {

			await next(context);

			return;

		}

		if (HttpVerbs.UnsafeVerbs.Contains(context.Request.Method)) {

			try {

				await antiforgery.ValidateRequestAsync(context);

			} catch (AntiforgeryValidationException) {

				context.Response.StatusCode = 400;

				await context.Response.WriteAsJsonAsync(new ProblemResult() {
					Status = HttpStatusCode.BadRequest.Value,
					Message = HttpStatusCode.BadRequest.Humanized,
					Detail = "Anti-forgery validation failed.",
				});

				return;

			}

		}

		await next(context);

	}

}
