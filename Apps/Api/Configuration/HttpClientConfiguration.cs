namespace Olympus.Api;

internal static class HttpClientConfiguration {

	internal static void AddHttpClient(this WebApplicationBuilder builder) {

		builder.Services.AddHttpContextAccessor();

		builder.Services.AddHttpClient();

	}

}
