using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Olympus.Core.Settings;

public static class AppSettingsHelper {

#if DEBUG
	private const AppSettingsType Environment = AppSettingsType.Development;
#else
	private const AppSettingsType Environment = AppSettingsType.Production;
#endif

	public static void LoadSettings(this IConfigurationBuilder manager) {

		var assembly = Assembly.GetCallingAssembly();

		AddJsonSource(manager, assembly, AppSettingsType.Global);
		AddJsonSource(manager, assembly, AppSettingsType.Common);
		AddJsonSource(manager, assembly, Environment);

		manager.AddEnvironmentVariables();
		manager.AddUserSecrets(assembly);

	}

	private static void AddJsonSource(IConfigurationBuilder builder, Assembly assembly, AppSettingsType type) {

		var resourceName = type switch {
			AppSettingsType.Global => AppSettings.EmbeddedSettings.Global,
			AppSettingsType.Common => AppSettings.EmbeddedSettings.Common,
			AppSettingsType.Development => AppSettings.EmbeddedSettings.Development,
			AppSettingsType.Production => AppSettings.EmbeddedSettings.Production,
			_ => null,
		};

		if (resourceName is not null) {

			var stream = assembly.GetManifestResourceStream(resourceName);
			if (stream is not null) builder.AddJsonStream(stream);

		}

		builder.AddJsonFile(AppSettings.EmbeddedSettings.GetByType(type), optional: true, reloadOnChange: true);

	}

	public static void AddSettings(this IServiceCollection services) {

		services.AddOptions<AppSettings>().BindConfiguration(AppSettings.SectionName).Configure<IConfiguration>(static (settings, configuration) => {

			settings.Database.Uri = configuration.GetValue("OLYMPUS_URI", settings.Database.Uri);
			settings.Database.Host = configuration.GetValue("OLYMPUS_HOST", settings.Database.Host);
			settings.Database.Port = configuration.GetValue("OLYMPUS_PORT", settings.Database.Port);
			settings.Database.Username = configuration.GetValue("OLYMPUS_USERNAME", settings.Database.Username);
			settings.Database.Password = configuration.GetValue("OLYMPUS_PASSWORD", settings.Database.Password);
			settings.Database.ConnectionString = configuration.GetValue("ConnectionStrings:Olympus", settings.Database.ConnectionString);

			settings.Storage.Uri = configuration.GetValue("STORAGE_URI", settings.Storage.Uri);
			settings.Storage.Host = configuration.GetValue("STORAGE_HOST", settings.Storage.Host);
			settings.Storage.Port = configuration.GetValue("STORAGE_PORT", settings.Storage.Port);
			settings.Storage.AccessKey = configuration.GetValue("STORAGE_ACCESSKEY", settings.Storage.AccessKey);
			settings.Storage.SecretKey = configuration.GetValue("STORAGE_SECRETKEY", settings.Storage.SecretKey);
			settings.Storage.ConnectionString = configuration.GetValue("ConnectionStrings:Storage", settings.Storage.ConnectionString);

			settings.Cache.Uri = configuration.GetValue("CACHE_URI", settings.Cache.Uri);
			settings.Cache.Host = configuration.GetValue("CACHE_HOST", settings.Cache.Host);
			settings.Cache.Port = configuration.GetValue("CACHE_PORT", settings.Cache.Port);
			settings.Cache.Password = configuration.GetValue("CACHE_PASSWORD", settings.Cache.Password);
			settings.Cache.ConnectionString = configuration.GetValue("ConnectionStrings:Cache", settings.Cache.ConnectionString);

		});

		services.AddSingleton(static provider => provider.GetRequiredService<IOptions<AppSettings>>().Value);

	}

}
