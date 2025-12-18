namespace Olympus.Api.ErrorHandling;

public class AppProblemDetailsWriter : IProblemDetailsWriter {

	public bool CanWrite(ProblemDetailsContext context) {

		return true;

	}

	public async ValueTask WriteAsync(ProblemDetailsContext context) {

		var httpContext = context.HttpContext;
		var problemDetails = context.ProblemDetails;

		var response = new ProblemResult() {
			Status = problemDetails.Status ?? httpContext.Response.StatusCode,
			Message = problemDetails.Title ?? ErrorsStrings.Values.UnknownError,
			Details = problemDetails.Detail,
		};

		httpContext.Response.StatusCode = response.Status;
		httpContext.Response.ContentType = ContentTypes.Json;

		await httpContext.Response.WriteAsJsonAsync(response, httpContext.RequestAborted);

	}

}
