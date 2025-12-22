using System.Text;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api.ErrorHandling;

public class GlobalExceptionHandler(IWebHostEnvironment environment) : IExceptionHandler {

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {

		var response = new ProblemResult();

		switch (exception) {

			case BadHttpRequestException:
				response.Status = HttpStatusCode.BadRequest.Value;
				response.Message = HttpStatusCode.BadRequest.Humanized;
				response.Detail = ErrorsStrings.Values.InvalidOrMalformedRequest;
				response.Debug = GetDebugDetails(exception);
				break;

			case UniqueConstraintException ex:
				response.Status = HttpStatusCode.Conflict.Value;
				response.Message = HttpStatusCode.Conflict.Humanized;
				response.Detail = string.Format(ErrorsStrings.Values.UniqueConstraintViolation, ex.ConstraintName);
				response.Debug = GetDebugDetails(exception);
				break;

			case ReferenceConstraintException ex:
				response.Status = HttpStatusCode.Conflict.Value;
				response.Message = HttpStatusCode.Conflict.Humanized;
				response.Detail = string.Format(ErrorsStrings.Values.ReferenceConstraintViolation, ex.ConstraintName);
				response.Debug = GetDebugDetails(exception);
				break;

			case CannotInsertNullException:
				response.Status = HttpStatusCode.BadRequest.Value;
				response.Message = HttpStatusCode.BadRequest.Humanized;
				response.Detail = ErrorsStrings.Values.CannotInsertNull;
				response.Debug = GetDebugDetails(exception);
				break;

			case MaxLengthExceededException:
				response.Status = HttpStatusCode.BadRequest.Value;
				response.Message = HttpStatusCode.BadRequest.Humanized;
				response.Detail = ErrorsStrings.Values.MaxLengthExceeded;
				response.Debug = GetDebugDetails(exception);
				break;

			case NumericOverflowException:
				response.Status = HttpStatusCode.BadRequest.Value;
				response.Message = HttpStatusCode.BadRequest.Humanized;
				response.Detail = ErrorsStrings.Values.NumericOverflow;
				response.Debug = GetDebugDetails(exception);
				break;

			case DbUpdateConcurrencyException:
				response.Status = HttpStatusCode.Conflict.Value;
				response.Message = HttpStatusCode.Conflict.Humanized;
				response.Detail = ErrorsStrings.Values.ConcurrencyConflict;
				response.Debug = GetDebugDetails(exception);
				break;

			case DbUpdateException ex:
				response.Status = HttpStatusCode.BadRequest.Value;
				response.Message = HttpStatusCode.BadRequest.Humanized;
				response.Detail = ex.Message;
				response.Debug = GetDebugDetails(exception)?.Crop("\r\nThe statement has been terminated.");
				break;

			default:
				response.Status = HttpStatusCode.InternalServerError.Value;
				response.Message = HttpStatusCode.InternalServerError.Humanized;
				response.Debug = GetDebugDetails(exception);
				break;

		}

		httpContext.Response.StatusCode = response.Status;
		await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

		return true;

	}

	private string? GetDebugDetails(Exception exception) {

		if (!environment.IsDevelopment()) return null;

		var detailsBuilder = new StringBuilder();

		detailsBuilder.Append(exception.Message);

		var inner = exception.InnerException;

		while (inner is not null) {

			detailsBuilder.Append(" -> ").Append(inner.Message);
			inner = inner.InnerException;

		}

		return detailsBuilder.ToString()
			.Replace("\"", "'")
			.Replace("\r", " ")
			.Replace("\n", " ")
			.Replace("  ", " ")
			.Replace("  ", " ")
			.Replace("  ", " ")
			.Trim();

	}

}
