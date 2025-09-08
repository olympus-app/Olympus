using Microsoft.AspNetCore.Http;

namespace Olympus.Api;

public class ProblemDetailsHandler : IProblemDetailsWriter {

	public bool CanWrite(ProblemDetailsContext context) => true;

	public async ValueTask WriteAsync(ProblemDetailsContext context) {

		var httpContext = context.HttpContext;
		var problemDetails = context.ProblemDetails;

		var response = new GlobalErrorResponse {
			Error = problemDetails.Status ?? httpContext.Response.StatusCode,
			Message = problemDetails.Title ?? AppErrors.Values.UnknownError,
			Details = problemDetails.Detail
			// Identifier = problemDetails.Extensions.TryGetValue("traceId", out var traceId) ? traceId?.ToString() : null
		};

		httpContext.Response.StatusCode = response.Error;
		httpContext.Response.ContentType = "application/json";

		await httpContext.Response.WriteAsJsonAsync(response, httpContext.RequestAborted);

	}

}
