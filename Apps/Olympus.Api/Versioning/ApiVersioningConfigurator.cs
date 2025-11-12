namespace Olympus.Api.Versioning;

public class ApiVersioningConfigurator : IConfigureOptions<ApiVersioningOptions> {

	public void Configure(ApiVersioningOptions options) {

		options.ReportApiVersions = true;
		options.AssumeDefaultVersionWhenUnspecified = true;
		options.ApiVersionReader = new HeaderApiVersionReader("api-version");

	}

}
