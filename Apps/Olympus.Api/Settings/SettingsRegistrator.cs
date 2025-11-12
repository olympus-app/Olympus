using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api.Settings;

public static class SettingsRegistrator {

	public static void AddSettings(this WebApplicationBuilder builder) {

		var settings = new AppSettings();
		var assembly = Assembly.GetCallingAssembly();
		var development = builder.Environment.IsDevelopment();
		var configuration = AppSettingsHelper.GetSettings(assembly, development);

		builder.Configuration.AddConfiguration(configuration);
		builder.Configuration.Bind(settings);
		builder.Services.AddSingleton(settings);

	}

}
