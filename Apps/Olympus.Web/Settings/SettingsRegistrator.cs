using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Olympus.Web.Settings;

public static class SettingsRegistrator {

	public static void AddSettings(this WebAssemblyHostBuilder builder) {

		var settings = new AppSettings();
		var assembly = Assembly.GetCallingAssembly();
		var development = builder.HostEnvironment.IsDevelopment();
		var configuration = AppSettingsHelper.GetSettings(assembly, development);

		builder.Configuration.AddConfiguration(configuration);
		builder.Configuration.Bind(settings);
		builder.Services.AddSingleton(settings);

	}

}
