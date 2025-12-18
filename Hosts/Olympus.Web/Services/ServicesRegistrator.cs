using Microsoft.AspNetCore.Components.Authorization;

namespace Olympus.Web.Services;

public static class ServicesRegistrator {

	public static void AddWebServices(this WebAssemblyHostBuilder builder) {

		builder.Services.AddSingleton<IFormFactor, FormFactor>();

		builder.Services.AddScoped<StorageService>();

		builder.Services.AddScoped<LocalStorageService>();

		builder.Services.AddScoped<SessionStorageService>();

		builder.Services.AddScoped<PasskeyService>();

		builder.Services.AddScoped<AntiforgeryService>();

		builder.Services.AddTransient<AntiforgeryHandler>();

		builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();

		builder.Services.AddScoped(static provider => (AuthenticationService)provider.GetRequiredService<AuthenticationStateProvider>());

		builder.Services.AddHttpClient<AuthClient>(client => { client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });

		builder.Services.AddHttpClient("Auth", options => { options.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); });

		builder.Services.AddHttpClient("API", options => options.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<AntiforgeryHandler>();

		builder.Services.AddScoped(static provider => provider.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

		builder.Services.AddAuthorizationCore(static options => {
			options.AddPolicy("Zeus.Writer", static policy => policy.RequireClaim("Permission", "Zeus.Writer"));
			options.AddPolicy("Zeus.Reader", static policy => policy.RequireClaim("Permission", "Zeus.Reader"));
		});

		builder.Services.AddCascadingAuthenticationState();

	}

}
