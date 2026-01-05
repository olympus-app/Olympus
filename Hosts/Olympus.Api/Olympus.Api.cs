namespace Olympus.Api;

public static class Api {

	public static void AddServices(this WebApplicationBuilder builder, AppHostInfo info) {

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

		builder.AddEndpointsServices();

		builder.AddDocumentationServices(info);

	}

	public static void BuildAndRun(this WebApplicationBuilder builder) {

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

		app.MapAuthenticationEndpoints();

		app.MapAuthorizationEndpoints();

		app.MapPasskeysEndpoints();

		app.MapTokensEndpoints();

		app.MapEndpoints();

		app.MapDocumentation();

		app.MapWebClient();

		app.Run();

	}

}
