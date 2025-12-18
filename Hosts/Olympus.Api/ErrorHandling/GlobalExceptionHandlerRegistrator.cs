using Microsoft.Extensions.Hosting;

namespace Olympus.Api.ErrorHandling;

public static class GlobalExceptionHandlerRegistrator {

	public static void AddGlobalExceptionHandlerServices(this WebApplicationBuilder builder) {

		builder.Services.AddProblemDetails();

		builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

		builder.Services.AddSingleton<IProblemDetailsWriter, AppProblemDetailsWriter>();

	}

	public static void UseGlobalExceptionHandlerMiddleware(this WebApplication app) {

		if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

		app.UseExceptionHandler();

	}

}
