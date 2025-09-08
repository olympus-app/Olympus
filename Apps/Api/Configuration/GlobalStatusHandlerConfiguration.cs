using System.Net;
using Microsoft.AspNetCore.Http;

namespace Olympus.Api;

internal static class GlobalStatusHandlerConfiguration {

	internal static void UseGlobalStatusHandler(this WebApplication app) {

		app.UseStatusCodePages(async statusCodeContext => {

			var context = statusCodeContext.HttpContext;

			if (context.Response.StatusCode >= HttpStatusCode.NotFound.ToInt() && context.Response.ContentLength is null) {

				var response = new GlobalErrorResponse {
					Error = context.Response.StatusCode,
					Message = ((HttpStatusCode)context.Response.StatusCode).ToString()
				};

				context.Response.ContentType = "application/json";
				await context.Response.WriteAsJsonAsync(response);

			}

		});

	}

}
