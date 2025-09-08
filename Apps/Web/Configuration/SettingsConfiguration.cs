using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Olympus.Web;

public static class SettingsConfiguration {

	public static void AddAppSettings(this WebAssemblyHostBuilder builder) {

		var settings = new AppSettings();
		var development = builder.HostEnvironment.IsDevelopment();
		var configuration = AppSettingsHelper.GetSettings(development);

		builder.Configuration.AddConfiguration(configuration);
		builder.Configuration.Bind(settings);
		builder.Services.AddSingleton(settings);

	}

}
