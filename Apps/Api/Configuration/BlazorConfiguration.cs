using Microsoft.Extensions.Hosting;

namespace Olympus.Api;

internal static class BlazorConfiguration {

	internal static void UseBlazorHosting(this WebApplication app) {

		if (app.Environment.IsDevelopment()) {

			app.UseWebAssemblyDebugging();

		}

		app.UseBlazorFrameworkFiles();

		app.UseStaticFiles();

	}

	internal static void UseHomepage(this WebApplication app) {

		app.MapFallbackToFile("api/{**slug}", string.Empty);

		app.MapFallbackToFile("{**slug}", "index.html");

	}

}
