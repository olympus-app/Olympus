using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Olympus.Web.Host;

public sealed class WebHost {

	public static async Task Main(string[] args) {

		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.AddAppSettings();

		builder.AddModules();

		builder.AddServices();

		// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

		await builder.RunAsync();

	}

}
