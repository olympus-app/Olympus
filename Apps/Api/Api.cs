namespace Olympus.Api;

public static class ApiCore {

	public static void AddServices(this WebApplicationBuilder builder) {

		builder.AddLocalization();

		builder.AddHttpClient();

		builder.AddDatabaseCache();

		builder.AddGlobalErrorHandler();

		builder.AddRateLimits();

		builder.AddControllers();

		builder.AddDocumentation();

	}

	public static void Run(this WebApplicationBuilder builder) {

		var app = builder.Build();

		app.UseLocalization();

		// app.UseRateLimits();

		app.UseHttpsRedirection();

		app.UseGlobalErrorHandler();

		// app.UseGlobalStatusHandler();

		app.UseControllers();

		app.UseBlazorHosting();

		app.UseDocumentation();

		app.UseHomepage();

		app.Run();

	}

}
