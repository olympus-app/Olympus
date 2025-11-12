namespace Olympus.Api.Services;

internal static class ServicesRegistrator {

	internal static void AddApiServices(this WebApplicationBuilder builder) {

		builder.Services.AddHttpContextAccessor();

		builder.Services.AddHttpClient();

	}

	internal static void UseApiServices(this WebApplication app) {

	}

}
