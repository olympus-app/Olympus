using Asp.Versioning.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Olympus.Api;

public class AppSwaggerUIOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerUIOptions> {

	public void Configure(SwaggerUIOptions options) {

		options.DefaultModelExpandDepth(2);
		options.DefaultModelsExpandDepth(1);
		options.DocExpansion(DocExpansion.List);
		options.SupportedSubmitMethods(null);
		options.ShowCommonExtensions();
		options.RoutePrefix = $"{AppConsts.ApiPrefix}/swagger";

		foreach (var version in provider.ApiVersionDescriptions) {

			var name = $"{AppConsts.ApiName} {version.GroupName}";
			var url = $"{AppConsts.ApiPath}/{version.GroupName}.json";
			options.SwaggerEndpoint(url, name);

		}

	}

}
