using System.Reflection;

namespace Olympus.Core.Settings;

public abstract class Settings : ISettings {

	private static bool SettingsLoaded;

#if DEBUG
	private const AppSettingsType Environment = AppSettingsType.Development;
#else
	private const AppSettingsType Environment = AppSettingsType.Production;
#endif

	public abstract void Configure(IConfiguration configuration);

	private static void AddSource(IConfigurationBuilder builder, Assembly assembly, AppSettingsType type) {

		var name = type.Name ?? string.Empty;
		var file = $"{name}Settings.json";
		var stream = assembly.GetManifestResourceStream(file);
		if (stream is not null) builder.AddJsonStream(stream);

		var path = Path.Combine("Properties", file);
		builder.AddJsonFile(path, true, true);

	}

	public static bool AddSources(IConfigurationBuilder builder, Assembly assembly) {

		AddSource(builder, assembly, AppSettingsType.Global);
		AddSource(builder, assembly, AppSettingsType.Common);
		AddSource(builder, assembly, Environment);

		builder.AddEnvironmentVariables();
		builder.AddUserSecrets(assembly);

		return true;

	}

	public static void AddSetting<TSetting>(IServiceCollection services, IConfigurationBuilder configuration, Assembly assembly) where TSetting : class, ISettings {

		if (!SettingsLoaded) SettingsLoaded = AddSources(configuration, assembly);

		services.AddOptions<TSetting>().Configure<IConfiguration>(static (settings, configuration) => settings.Configure(configuration));

		services.AddSingleton<ISettings>(static services => services.GetRequiredService<IOptions<TSetting>>().Value);

		services.AddSingleton(static services => services.GetRequiredService<IOptions<TSetting>>().Value);

	}

}
