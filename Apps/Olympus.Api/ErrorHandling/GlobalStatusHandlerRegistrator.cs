using System.Net;
using Microsoft.AspNetCore.Http;

namespace Olympus.Api.ErrorHandling;

internal static class GlobalStatusHandlerRegistrator {

	internal static void UseGlobalStatusHandlerServices(this WebApplication app) {

		app.UseStatusCodePages(async statusCodeContext => {

			var context = statusCodeContext.HttpContext;

			if (context.Response.StatusCode >= 400 && context.Response.ContentLength is null) {

				var response = new ErrorResult {
					Status = context.Response.StatusCode,
					Message = ((HttpStatusCode)context.Response.StatusCode).Humanize() + "."
				};

				context.Response.ContentType = ContentTypes.Json;
				await context.Response.WriteAsJsonAsync(response);

			}

		});

	}

}
