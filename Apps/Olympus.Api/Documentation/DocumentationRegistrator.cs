namespace Olympus.Api.Documentation;

internal static class DocumentationRegistrator {

	internal static void AddDocumentationServices(this WebApplicationBuilder builder) {

		builder.Services.ConfigureOptions<SwaggerConfigurator>();

		builder.Services.ConfigureOptions<SwaggerGenConfigurator>();

		builder.Services.ConfigureOptions<SwaggerUIConfigurator>();

		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddSwaggerGen();

	}

	internal static void UseDocumentationServices(this WebApplication app) {

		app.UseSwagger();

		app.UseSwaggerUI();

	}

}
