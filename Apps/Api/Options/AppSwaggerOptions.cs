using Swashbuckle.AspNetCore.Swagger;

namespace Olympus.Api;

public class AppSwaggerOptions : IConfigureOptions<SwaggerOptions> {

	public void Configure(SwaggerOptions options) {

		options.RouteTemplate = $"{AppConsts.ApiPath}/{{documentName}}.{{extension:regex(^(json|ya?ml)$)}}";

	}

}
