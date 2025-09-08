using Asp.Versioning.ApiExplorer;
using Scalar.AspNetCore;

namespace Olympus.Api;

public class AppScalarOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<ScalarOptions> {

	public void Configure(ScalarOptions options) {

		options.WithModels(true);
		options.WithDotNetFlag(true);
		options.WithClientButton(false);
		options.WithDefaultOpenAllTags(false);
		options.WithTitle(AppConsts.ApiDocsTitle);
		options.WithTagSorter(TagSorter.Alpha);
		options.WithLayout(ScalarLayout.Modern);
		options.WithTheme(ScalarTheme.None);

		foreach (var version in provider.ApiVersionDescriptions) {

			var name = $"{AppConsts.ApiName} {version.GroupName}";
			var url = $"{AppConsts.ApiPath}/{version.GroupName}.json";
			options.AddDocument(name, null, url);

		}

	}

}
