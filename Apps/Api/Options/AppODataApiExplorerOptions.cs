using Asp.Versioning.ApiExplorer;

namespace Olympus.Api;

public class AppODataApiExplorerOptions : IConfigureOptions<ODataApiExplorerOptions> {

	public void Configure(ODataApiExplorerOptions options) {

		options.GroupNameFormat = AppConsts.ApiVersionFormat;

	}

}
