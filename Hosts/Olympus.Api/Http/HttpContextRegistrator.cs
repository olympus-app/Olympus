namespace Olympus.Api.Http;

public static class HttpContextRegistrator {

	public static void AddHttpContextServices(this WebApplicationBuilder builder) {

		builder.Services.AddHttpContextAccessor();

	}

}
