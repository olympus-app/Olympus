namespace Olympus.Api.Http;

public static class HttpClientRegistrator {

	public static void AddHttpClientServices(this WebApplicationBuilder builder) {

		builder.Services.AddHttpClient();

	}

}
