namespace Olympus.Api;

public static class Api {

	public static void AddServices(this WebApplicationBuilder builder) {

		builder.AddApiServices();

		builder.AddProtectionServices();

		builder.AddGlobalErrorHandlerServices();

		builder.AddDatabaseServices();

		builder.AddDatabaseCacheServices();

		builder.AddControllersServices();

		builder.AddRoutingServices();

		builder.AddDocumentationServices();

	}

	public static void BuildAndRun(this WebApplicationBuilder builder) {

		var app = builder.Build();

		app.UseApiServices();

		app.UseProtectionServices();

		app.UseGlobalErrorHandlerServices();

		app.UseGlobalStatusHandlerServices();

		app.UseDocumentationServices();

		app.UseWebClientServices();

		app.UseRoutingServices();

		app.UseControllersServices();

		app.MapWebClient();

		app.Run();

	}

}
