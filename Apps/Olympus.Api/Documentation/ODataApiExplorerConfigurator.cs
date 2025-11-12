using Asp.Versioning.ApiExplorer;

namespace Olympus.Api.Documentation;

public class ODataApiExplorerConfigurator : IConfigureOptions<ODataApiExplorerOptions> {

	public void Configure(ODataApiExplorerOptions options) {

		options.GroupNameFormat = AppConsts.ApiVersionFormat;
		options.FormatGroupName = (group, version) => $"{group} - {version}";

	}

}
