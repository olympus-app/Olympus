using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api;

public class GlobalErrorHandler(IWebHostEnvironment environment) : IExceptionHandler {

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {

		var response = new GlobalErrorResponse();

		switch (exception) {

			case AppException ex:
				response.Error = (ex.StatusCode ?? HttpStatusCode.InternalServerError).ToInt();
				response.Message = ex.Message ?? AppErrors.Values.UnknownError;
				response.Details = ex.InnerException?.Message;
				response.Localizer = ex.Key;
				response.Resource = ex.Resource;
				response.Identifier = ex.Identifier;
				break;

			case DbUpdateException ex:
				response.Error = HttpStatusCode.BadRequest.ToInt();
				response.Message = ex.Message;
				response.Details = ex.InnerException?.Message.Replace("\r\nThe statement has been terminated.", "");
				break;

			default:
				response.Error = HttpStatusCode.InternalServerError.ToInt();
				response.Message = AppErrors.Values.UnknownError;
				response.Details = environment.IsDevelopment() ? "[Debug Only]: " + exception.Message + (exception.InnerException?.Message is null ? string.Empty : "\n" + exception.InnerException.Message) : null;
				break;

		}

		httpContext.Response.StatusCode = response.Error;
		await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

		return true;

	}

}
