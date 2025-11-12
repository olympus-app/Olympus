using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api.ErrorHandling;

public class GlobalErrorHandler(IWebHostEnvironment environment) : IExceptionHandler {

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {

		var response = new ErrorResult();

		switch (exception) {

			case AppBackendException ex:
				response.Status = (ex.StatusCode ?? HttpStatusCode.InternalServerError).Value;
				response.Message = ex.Message ?? AppErrors.Values.UnknownError;
				response.Details = ex.InnerException?.Message;
				response.Code = ex.Localizer;
				break;

			case DbUpdateException ex:
				response.Status = HttpStatusCode.BadRequest.Value;
				response.Message = ex.Message;
				response.Details = ex.InnerException?.Message.Replace("\r\nThe statement has been terminated.", "");
				break;

			default:
				response.Status = HttpStatusCode.InternalServerError.Value;
				response.Message = AppErrors.Values.UnknownError;
				response.Details = environment.IsDevelopment() ? "[Debug Only]: " + exception.Message + (exception.InnerException?.Message is null ? string.Empty : "\n" + exception.InnerException.Message) : null;
				break;

		}

		httpContext.Response.StatusCode = response.Status;
		await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

		return true;

	}

}
