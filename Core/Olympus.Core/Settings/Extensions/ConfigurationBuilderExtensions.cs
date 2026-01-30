using System.Reflection;

namespace Olympus.Core.Settings;

public static class ConfigurationBuilderExtensions {

#if DEBUG
	private const AppSettingsType Environment = AppSettingsType.Development;
#else
	private const AppSettingsType Environment = AppSettingsType.Production;
#endif

	extension(IConfigurationBuilder builder) {

		private void LoadSetting(Assembly assembly, AppSettingsType type) {

			var name = type.Name ?? string.Empty;
			var file = $"{name}Settings.json";
			var stream = assembly.GetManifestResourceStream(file);
			if (stream is not null) builder.AddJsonStream(stream);

			var path = Path.Combine("Properties", file);
			builder.AddJsonFile(path, true, true);

		}

		public bool LoadSettings(Assembly assembly) {

			builder.LoadSetting(assembly, AppSettingsType.Global);
			builder.LoadSetting(assembly, AppSettingsType.Common);
			builder.LoadSetting(assembly, Environment);

			builder.AddEnvironmentVariables();
			builder.AddUserSecrets(assembly);

			return true;

		}

	}

}
