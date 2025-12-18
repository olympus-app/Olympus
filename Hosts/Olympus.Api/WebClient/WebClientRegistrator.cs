using Microsoft.Extensions.Hosting;

namespace Olympus.Api.WebClient;

public static class WebClientRegistrator {

	public static void UseWebClientMiddleware(this WebApplication app) {

		if (app.Environment.IsDevelopment()) app.UseWebAssemblyDebugging();

	}

	public static void MapWebClient(this WebApplication app) {

		app.MapStaticAssets();

		var authPath = "/" + Routes.Auth.TrimStart('/');
		var apiPath = "/" + Routes.Api.TrimStart('/');

		app.MapWhen(context =>
			!context.Request.Path.StartsWithSegments(authPath, StringComparison.OrdinalIgnoreCase)
			&& !context.Request.Path.StartsWithSegments(apiPath, StringComparison.OrdinalIgnoreCase),
			static clientBranch => {
				clientBranch.UseRouting();
				clientBranch.UseEndpoints(static endpoints => {
					endpoints.MapFallbackToFile(Routes.Index);
				});
			}
		);

	}

}
