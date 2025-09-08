using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Olympus.Kernel;

public static class AppSettingsHelper {

	private static Stream GetSettingStream(Assembly assembly, AppSettingsType settings) {

		return settings switch {

			AppSettingsType.Global => assembly.GetManifestResourceStream("Settings.Global.json"),
			AppSettingsType.Default => assembly.GetManifestResourceStream("Settings.Default.json"),
			AppSettingsType.Development => assembly.GetManifestResourceStream("Settings.Development.json"),
			AppSettingsType.Production => assembly.GetManifestResourceStream("Settings.Production.json"),
			_ => throw new AppSettingsException(AppErrors.Keys.CannotLoadSettings, settings)

		} ?? throw new AppSettingsException(AppErrors.Keys.CannotLoadSettings, settings);

	}

	private static void LoadSettings(this IConfigurationBuilder builder, Assembly assembly, AppSettingsType settings) {

		var stream = GetSettingStream(assembly, settings);

		builder.AddJsonStream(stream);
		builder.AddJsonFile($"Settings\\{settings}.json", true);

	}

	public static IConfigurationRoot GetSettings(bool development = false) {

		var config = new ConfigurationBuilder();
		var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

		config.LoadSettings(assembly, AppSettingsType.Global);
		config.LoadSettings(assembly, AppSettingsType.Default);
		config.LoadSettings(assembly, development ? AppSettingsType.Development : AppSettingsType.Production);

		return config.Build();

	}

}
