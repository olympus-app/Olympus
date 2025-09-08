using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Olympus.Api;

public static class SettingsConfiguration {

	public static void AddAppSettings(this WebApplicationBuilder builder) {

		var settings = new AppSettings();
		var development = builder.Environment.IsDevelopment();
		var configuration = AppSettingsHelper.GetSettings(development);

		builder.Configuration.AddConfiguration(configuration);
		builder.Configuration.Bind(settings);
		builder.Services.AddSingleton(settings);

	}

}
