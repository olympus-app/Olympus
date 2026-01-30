namespace Olympus.Api;

public static class Api {

	public static void AddApiServices(this WebApplicationBuilder builder, ApiHostInfo info) {

		builder.AddGlobalExceptionHandlerServices();

		builder.AddTransportCompressionServices();

		builder.AddLocalizationServices();

		builder.AddHttpClientServices();

		builder.AddHttpContextServices();

		builder.AddRoutingServices();

		builder.AddCacheServices();

		builder.AddDatabaseServices(info);

		builder.AddStorageServices();

		builder.AddQueryingServices();

		builder.AddJsonServices();

		builder.AddAuthenticationServices();

		builder.AddAuthorizationServices();

		builder.AddDataProtectionServices();

		builder.AddEndpointsServices(info);

		builder.AddDocumentationServices(info);

	}

	public static void BuildAndRun(this WebApplicationBuilder builder, ApiHostInfo info) {

		var app = builder.Build();

		app.UpdateDatabase();

		app.UseGlobalExceptionHandlerMiddleware();

		app.UseTransportSecurityMiddleware();

		app.UseTransportCompressionMiddleware();

		app.UseRoutingMiddleware();

		app.UseAuthenticationMiddleware();

		app.UseAuthorizationMiddleware();

		app.UseMetadataMiddleware();

		app.UseWebClientMiddleware();

		app.MapEndpoints(info);

		app.MapDocumentation();

		app.MapWebClient();

		app.Run();

	}

}
