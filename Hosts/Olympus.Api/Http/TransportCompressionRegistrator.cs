using Microsoft.Extensions.Hosting;

namespace Olympus.Api.Http;

public static class TransportCompressionRegistrator {

	public static void AddTransportCompressionServices(this WebApplicationBuilder builder) {

		builder.Services.AddResponseCompression(static options => {
			options.EnableForHttps = true;
		});

	}

	public static void UseTransportCompressionMiddleware(this WebApplication app) {

		if (app.Environment.IsProduction()) app.UseResponseCompression();

	}

}
