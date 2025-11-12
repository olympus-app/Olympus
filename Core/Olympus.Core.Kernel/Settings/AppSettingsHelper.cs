using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Olympus.Core.Kernel.Settings;

public static class AppSettingsHelper {

	private static Stream GetSettingStream(Assembly assembly, AppSettingsEnvironment environment) {

		return environment switch {

			AppSettingsEnvironment.Global => assembly.GetManifestResourceStream("Settings.Global.json"),
			AppSettingsEnvironment.Common => assembly.GetManifestResourceStream("Settings.Common.json"),
			AppSettingsEnvironment.Development => assembly.GetManifestResourceStream("Settings.Development.json"),
			AppSettingsEnvironment.Production => assembly.GetManifestResourceStream("Settings.Production.json"),
			_ => throw new AppSettingsException(AppErrors.Keys.CannotLoadSettings, environment)

		} ?? throw new AppSettingsException(AppErrors.Keys.CannotLoadSettings, environment);

	}

	private static void LoadSettings(IConfigurationBuilder builder, Assembly assembly, AppSettingsEnvironment environment) {

		var stream = GetSettingStream(assembly, environment);

		builder.AddJsonStream(stream);
		builder.AddJsonFile($"Settings\\{environment}.json", true);

	}

	public static IConfigurationRoot GetSettings(Assembly assembly, bool development = false) {

		var config = new ConfigurationBuilder();

		LoadSettings(config, assembly, AppSettingsEnvironment.Global);
		LoadSettings(config, assembly, AppSettingsEnvironment.Common);
		LoadSettings(config, assembly, development ? AppSettingsEnvironment.Development : AppSettingsEnvironment.Production);

		return config.Build();

	}

}
