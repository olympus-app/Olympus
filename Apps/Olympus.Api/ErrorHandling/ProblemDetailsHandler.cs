using Microsoft.AspNetCore.Http;

namespace Olympus.Api.ErrorHandling;

public class ProblemDetailsHandler : IProblemDetailsWriter {

	public bool CanWrite(ProblemDetailsContext context) {

		return true;

	}

	public async ValueTask WriteAsync(ProblemDetailsContext context) {

		var httpContext = context.HttpContext;
		var problemDetails = context.ProblemDetails;

		var response = new ErrorResult {
			Status = problemDetails.Status ?? httpContext.Response.StatusCode,
			Message = problemDetails.Title ?? AppErrors.Values.UnknownError,
			Details = problemDetails.Detail
		};

		httpContext.Response.StatusCode = response.Status;
		httpContext.Response.ContentType = ContentTypes.Json;

		await httpContext.Response.WriteAsJsonAsync(response, httpContext.RequestAborted);

	}

}
