using Asp.Versioning.OData;
using Microsoft.AspNetCore.OData.Formatter.Serialization;

namespace Olympus.Api;

public class AppODataApiVersioningOptions(IEnumerable<IAppModuleOptions> modules) : IConfigureOptions<ODataApiVersioningOptions> {

	public void Configure(ODataApiVersioningOptions options) {

		foreach (var module in modules) {

			var prefix = module.ApiPrefix;

			options.AddRouteComponents(prefix, services => {
				services.AddSingleton<ODataErrorSerializer, ODataErrorHandler>();
			});

		}

	}

}
