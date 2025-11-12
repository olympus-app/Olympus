using Microsoft.AspNetCore.OData;

namespace Olympus.Api.Controllers;

internal static class ControllersRegistrator {

	internal static void AddControllersServices(this WebApplicationBuilder builder) {

		builder.Services.AddControllers().AddOData();

		builder.Services.AddApiVersioning().AddOData().AddODataApiExplorer();

		builder.Services.ConfigureOptions<MvcConfigurator>();

		builder.Services.ConfigureOptions<ODataConfigurator>();

		builder.Services.ConfigureOptions<ApiVersioningConfigurator>();

		builder.Services.ConfigureOptions<ODataApiVersioningConfigurator>();

		builder.Services.ConfigureOptions<ODataApiExplorerConfigurator>();

	}

	internal static void UseControllersServices(this WebApplication app) {

		app.UseODataQueryRequest();

		app.MapControllers();

	}

}
