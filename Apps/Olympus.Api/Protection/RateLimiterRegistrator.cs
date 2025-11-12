namespace Olympus.Api.Protection;

internal static class RateLimiterRegistrator {

	internal static void AddRateLimiterServices(this WebApplicationBuilder builder) {

		builder.Services.AddRateLimiter();

		builder.Services.ConfigureOptions<RateLimiterConfigurator>();

	}

	internal static void UseRateLimiterServices(this WebApplication app) {

		app.UseRateLimiter();

	}

}
