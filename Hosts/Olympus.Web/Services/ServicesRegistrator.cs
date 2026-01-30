using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace Olympus.Web.Services;

public static class ServicesRegistrator {

	public static void AddWebServices(this WebAssemblyHostBuilder builder) {

		builder.Services.AddScoped<StorageService>();

		builder.Services.AddScoped<LocalStorageService>();

		builder.Services.AddScoped<SessionStorageService>();

		builder.Services.AddScoped<PasskeyService>();

		builder.Services.AddScoped<AntiforgeryService>();

		builder.Services.AddTransient<AntiforgeryHandler>();

		builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();

		builder.Services.AddScoped(static provider => (AppAuthenticationStateProvider)provider.GetRequiredService<AuthenticationStateProvider>());

		builder.Services.AddHttpClient<AuthClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

		builder.Services.AddHttpClient<ApiClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<AntiforgeryHandler>();

		builder.Services.AddHttpClient("API", options => options.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<AntiforgeryHandler>();

		builder.Services.AddScoped(static provider => provider.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

		builder.Services.AddAuthorizationCore(static options => {
			options.AddPolicy("Zeus.Writer", static policy => policy.RequireClaim("Permission", "Zeus.Writer"));
			options.AddPolicy("Zeus.Reader", static policy => policy.RequireClaim("Permission", "Zeus.Reader"));
			options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
		});

		builder.Services.AddCascadingAuthenticationState();

		builder.Logging.SetMinimumLevel(LogLevel.Warning);

	}

}
