using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api.ErrorHandling;

internal static class GlobalErrorHandlerRegistrator {

	internal static void AddGlobalErrorHandlerServices(this WebApplicationBuilder builder) {

		builder.Services.AddProblemDetails();

		builder.Services.AddExceptionHandler<GlobalErrorHandler>();

		builder.Services.AddSingleton<IProblemDetailsWriter, ProblemDetailsHandler>();

	}

	internal static void UseGlobalErrorHandlerServices(this WebApplication app) {

		if (app.Environment.IsDevelopment()) {

			app.UseDeveloperExceptionPage();

		}

		app.UseExceptionHandler();

	}

}
