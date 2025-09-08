namespace Olympus.Api;

public class AppApiVersioningOptions : IConfigureOptions<ApiVersioningOptions> {

	public void Configure(ApiVersioningOptions options) {

		options.ReportApiVersions = true;
		options.AssumeDefaultVersionWhenUnspecified = true;

	}

}
