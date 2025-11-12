using Microsoft.Extensions.Hosting;

namespace Olympus.Api.WebClient;

internal static class WebClientRegistrator {

	internal static void UseWebClientServices(this WebApplication app) {

		if (app.Environment.IsDevelopment()) {

			app.UseWebAssemblyDebugging();

		}

	}

	internal static void MapWebClient(this WebApplication app) {

		app.MapStaticAssets();

		app.MapFallbackToFile("api/{**slug}", string.Empty);

		app.MapFallbackToFile("{**slug}", AppConsts.IndexFileName);

	}

}
