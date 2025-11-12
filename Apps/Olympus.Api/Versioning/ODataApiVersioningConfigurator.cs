using Asp.Versioning.OData;

namespace Olympus.Api.Versioning;

public class ODataApiVersioningConfigurator(IEnumerable<IAppModuleOptions> modules) : IConfigureOptions<ODataApiVersioningOptions> {

	public void Configure(ODataApiVersioningOptions options) {

		foreach (var module in modules) {

			var prefix = module.ApiPrefix;
			options.AddRouteComponents(prefix);

		}

	}

}
