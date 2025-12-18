namespace Olympus.Api.Metadata;

public class MetadataMiddleware(RequestDelegate next) {

	private readonly RequestDelegate Next = next;

	private static readonly Dictionary<string, string> AppHeaders = new() {
		{ Headers.AppVersion, AppSettings.Version },
		{ Headers.AppVersionShort, AppSettings.VersionShort },
		{ Headers.AppBuildNumber, AppSettings.BuildNumber.ToString() },
		{ Headers.AppBuildNumberForced, AppSettings.BuildNumberForced.ToString() },
	};

	public Task InvokeAsync(HttpContext context) {

		var headers = context.Response.Headers;

		foreach (var header in AppHeaders) {

			if (!headers.ContainsKey(header.Key)) {

				headers.Append(header.Key, header.Value);

			}

		}

		return Next(context);

	}

}
