using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api.Routing;

internal static class RoutingRegistrator {

	internal static void AddRoutingServices(this WebApplicationBuilder builder) {

		builder.Services.AddRouting();

		builder.Services.ConfigureOptions<RoutingConfigurator>();

		builder.Services.AddTransient<IApplicationModelProvider, RoutingProvider>();

	}

	internal static void UseRoutingServices(this WebApplication app) {

		if (app.Environment.IsDevelopment()) {

			app.UseODataRouteDebug($"${AppConsts.ApiPrefix}");

		}

		app.UseRouting();

	}

}
