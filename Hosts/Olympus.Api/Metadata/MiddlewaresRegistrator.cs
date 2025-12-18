namespace Olympus.Api.Metadata;

public static class MiddlewaresRegistrator {

	public static IApplicationBuilder UseMetadataMiddleware(this IApplicationBuilder app) {

		return app.UseMiddleware<MetadataMiddleware>();

	}

}
