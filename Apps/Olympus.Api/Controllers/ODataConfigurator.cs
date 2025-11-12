using Microsoft.AspNetCore.OData;

namespace Olympus.Api.Controllers;

public class ODataConfigurator : IConfigureOptions<ODataOptions> {

	public void Configure(ODataOptions options) {

		options.EnableQueryFeatures();
		options.TimeZone = TimeZoneInfo.Utc;
		options.EnableAttributeRouting = true;
		options.EnableNoDollarQueryOptions = true;
		options.RouteOptions.EnableKeyAsSegment = true;
		options.RouteOptions.EnableKeyInParenthesis = false;
		options.RouteOptions.EnableDollarValueRouting = true;
		options.RouteOptions.EnableDollarCountRouting = true;
		options.RouteOptions.EnableQualifiedOperationCall = false;
		options.RouteOptions.EnableUnqualifiedOperationCall = true;
		options.RouteOptions.EnableActionNameCaseInsensitive = true;
		options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
		options.RouteOptions.EnableControllerNameCaseInsensitive = true;
		options.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;

	}

}
