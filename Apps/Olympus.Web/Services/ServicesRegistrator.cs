namespace Olympus.Web.Services;

internal static class ServicesRegistrator {

	internal static void AddWebServices(this WebAssemblyHostBuilder builder) {

		builder.Services.AddScoped(services => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

		builder.Services.AddSingleton<IFormFactor, FormFactor>();

	}

}
