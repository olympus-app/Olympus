using Microsoft.Extensions.Configuration;

namespace Olympus.Native;

public static class SettingsConfiguration
{

	public static void AddAppSettings(this MauiAppBuilder builder)
	{

		var settings = new AppSettings();

#if DEBUG
		var development = true;
#else
		var development = false;
#endif

		var configuration = AppSettingsHelper.GetSettings(development);

		builder.Configuration.AddConfiguration(configuration);
		builder.Configuration.Bind(settings);
		builder.Services.AddSingleton(settings);

	}

}
