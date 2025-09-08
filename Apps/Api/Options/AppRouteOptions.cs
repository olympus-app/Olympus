namespace Olympus.Api;

public class AppRouteOptions : IConfigureOptions<RouteOptions> {

	public void Configure(RouteOptions options) {

		options.LowercaseUrls = true;
		options.LowercaseQueryStrings = true;

	}

}
