using Scalar.AspNetCore;
using static Olympus.Shared.AppConsts;

namespace Olympus.Api;

internal static class DocumentationConfiguration {

	internal static void AddDocumentation(this WebApplicationBuilder builder) {

		builder.Services.ConfigureOptions<AppSwaggerOptions>();

		builder.Services.ConfigureOptions<AppSwaggerGenOptions>();

		builder.Services.ConfigureOptions<AppSwaggerUIOptions>();

		builder.Services.ConfigureOptions<AppScalarOptions>();

		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddSwaggerGen();

	}

	internal static void UseDocumentation(this WebApplication app) {

		app.UseSwagger();

		app.UseSwaggerUI();

		app.MapScalarApiReference(ApiDocsPath);

	}

}
