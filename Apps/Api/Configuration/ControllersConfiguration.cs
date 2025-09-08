using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api;

internal static class ControllersConfiguration {

	internal static void AddControllers(this WebApplicationBuilder builder) {

		builder.Services.AddTransient<IApplicationModelProvider, AppModulesRoutingProvider>();

		builder.Services.ConfigureOptions<AppControllersOptions>();

		builder.Services.ConfigureOptions<AppODataOptions>();

		builder.Services.ConfigureOptions<AppApiVersioningOptions>();

		builder.Services.ConfigureOptions<AppODataApiVersioningOptions>();

		builder.Services.ConfigureOptions<AppODataApiExplorerOptions>();

		builder.Services.ConfigureOptions<AppRouteOptions>();

		builder.Services.AddControllers().AddOData();

		builder.Services.AddApiVersioning().AddOData().AddODataApiExplorer();

		builder.Services.AddRouting();

	}

	internal static void UseControllers(this WebApplication app) {

		if (app.Environment.IsDevelopment()) {

			app.UseODataRouteDebug($"${AppConsts.ApiPrefix}");

		}

		app.UseODataQueryRequest();

		app.UseRouting();

		app.MapControllers();

	}

}
