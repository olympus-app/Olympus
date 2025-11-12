namespace Olympus.Api.Routing;

public class RoutingConfigurator : IConfigureOptions<RouteOptions> {

	public void Configure(RouteOptions options) {

		options.LowercaseUrls = true;
		options.LowercaseQueryStrings = true;

	}

}
