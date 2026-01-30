using Microsoft.Extensions.Hosting;

namespace Olympus.Api.Routing;

public static class RoutingRegistrator {

	public static void AddRoutingServices(this WebApplicationBuilder builder) {

		builder.Services.AddRouting(static options => {

			options.AppendTrailingSlash = false;
			options.LowercaseQueryStrings = true;
			options.LowercaseUrls = true;

		});

	}

	public static void UseRoutingMiddleware(this WebApplication app) {

		if (app.Environment.IsDevelopment()) app.MapGet(AppRoutes.ApiRoutes, RouteDebugger.DebugAsync);

		app.UseRouting();

	}

}
