using Swashbuckle.AspNetCore.Swagger;

namespace Olympus.Api.Documentation;

public class SwaggerConfigurator : IConfigureOptions<SwaggerOptions> {

	public void Configure(SwaggerOptions options) {

		options.RouteTemplate = $"{AppConsts.ApiPath}/{{documentName}}.{{extension:regex(^(json|ya?ml)$)}}";

	}

}
