using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api;

internal static class GlobalErrorHandlerConfiguration {

	internal static void AddGlobalErrorHandler(this WebApplicationBuilder builder) {

		builder.Services.AddProblemDetails();

		builder.Services.AddExceptionHandler<GlobalErrorHandler>();

		builder.Services.AddSingleton<IProblemDetailsWriter, ProblemDetailsHandler>();

	}

	internal static void UseGlobalErrorHandler(this WebApplication app) {

		if (app.Environment.IsDevelopment()) {

			app.UseDeveloperExceptionPage();

		}

		app.UseExceptionHandler();

	}

}
