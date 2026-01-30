namespace Olympus.Api.Metadata;

public class MetadataMiddleware(RequestDelegate next) {

	private readonly RequestDelegate Next = next;

	private static readonly Dictionary<string, string> AppHeaders = new() {
		{ HttpHeaders.AppVersion, AppSettings.Version },
		{ HttpHeaders.AppVersionShort, AppSettings.VersionShort },
		{ HttpHeaders.AppBuildNumber, AppSettings.BuildNumber.ToString() },
		{ HttpHeaders.AppBuildNumberForced, AppSettings.BuildNumberForced.ToString() },
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
