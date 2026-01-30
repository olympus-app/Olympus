using Microsoft.Extensions.Hosting;

namespace Olympus.Api.WebClient;

public static class WebClientRegistrator {

	public static void UseWebClientMiddleware(this WebApplication app) {

		if (app.Environment.IsDevelopment()) app.UseWebAssemblyDebugging();

	}

	public static void MapWebClient(this WebApplication app) {

		app.MapStaticAssets().ShortCircuit();

		app.MapGroup(AppRoutes.Auth).MapFallback(static () => Results.NotFound());
		app.MapGroup(AppRoutes.Api).MapFallback(static () => Results.NotFound());

		app.MapFallbackToFile(AppRoutes.IndexFile);

	}

}
