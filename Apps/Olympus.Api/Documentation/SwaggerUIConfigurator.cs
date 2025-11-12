using Asp.Versioning.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Olympus.Api.Documentation;

public class SwaggerUIConfigurator(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerUIOptions> {

	public void Configure(SwaggerUIOptions options) {

		options.ShowCommonExtensions();
		options.DefaultModelExpandDepth(2);
		options.DefaultModelsExpandDepth(0);
		options.SupportedSubmitMethods(null);
		options.DocExpansion(DocExpansion.List);
		options.RoutePrefix = AppConsts.ApiDocsPrefix;

		foreach (var version in provider.ApiVersionDescriptions) {

			if (version.IsModuleGroup()) {

				options.SwaggerEndpoint(version.GetDocumentPath(), version.GetGroupFullName());
			}

		}

	}

}
