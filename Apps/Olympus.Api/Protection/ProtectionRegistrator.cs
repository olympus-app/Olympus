using Microsoft.Extensions.Hosting;

namespace Olympus.Api.Protection;

internal static class ProtectionRegistrator {

	internal static void AddProtectionServices(this WebApplicationBuilder builder) {

		// builder.Services.AddAntiforgery();

	}

	internal static void UseProtectionServices(this WebApplication app) {

		if (app.Environment.IsProduction()) {

			app.UseHsts();

		}

		app.UseHttpsRedirection();

		// app.UseAntiforgery();

	}

}
