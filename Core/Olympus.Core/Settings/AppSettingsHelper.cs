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

			settings.Database.Host = configuration.GetValueFromEnvironment(settings.Database.Host, $"{DatabaseSettings.DatabaseName}_{nameof(DatabaseSettings.Host)}");
			settings.Database.Port = configuration.GetValueFromEnvironment(settings.Database.Port, $"{DatabaseSettings.DatabaseName}_{nameof(DatabaseSettings.Port)}");
			settings.Database.Username = configuration.GetValueFromEnvironment(settings.Database.Username, $"{DatabaseSettings.DatabaseName}_{nameof(DatabaseSettings.Username)}");
			settings.Database.Password = configuration.GetValueFromEnvironment(settings.Database.Password, $"{DatabaseSettings.DatabaseName}_{nameof(DatabaseSettings.Password)}");
			settings.Database.ConnectionString = configuration.GetConnectionStringFromEnvironment(settings.Database.ConnectionString, DatabaseSettings.DatabaseName);

			settings.Storage.Host = configuration.GetValueFromEnvironment(settings.Storage.Host, $"{StorageSettings.ServiceName}_{nameof(StorageSettings.Host)}");
			settings.Storage.Port = configuration.GetValueFromEnvironment(settings.Storage.Port, $"{StorageSettings.ServiceName}_{nameof(StorageSettings.Port)}");
			settings.Storage.AccessKey = configuration.GetValueFromEnvironment(settings.Storage.AccessKey, $"{StorageSettings.ServiceName}_{nameof(StorageSettings.AccessKey)}");
			settings.Storage.SecretKey = configuration.GetValueFromEnvironment(settings.Storage.SecretKey, $"{StorageSettings.ServiceName}_{nameof(StorageSettings.SecretKey)}");
			settings.Storage.ConnectionString = configuration.GetConnectionStringFromEnvironment(settings.Storage.ConnectionString, StorageSettings.ServiceName);

			settings.Cache.Host = configuration.GetValueFromEnvironment(settings.Cache.Host, $"{CacheSettings.ServiceName}_{nameof(CacheSettings.Host)}");
			settings.Cache.Port = configuration.GetValueFromEnvironment(settings.Cache.Port, $"{CacheSettings.ServiceName}_{nameof(CacheSettings.Port)}");
			settings.Cache.Password = configuration.GetValueFromEnvironment(settings.Cache.Password, $"{CacheSettings.ServiceName}_{nameof(CacheSettings.Password)}");
			settings.Cache.ConnectionString = configuration.GetConnectionStringFromEnvironment(settings.Cache.ConnectionString, CacheSettings.ServiceName);

		});

		services.AddSingleton(static provider => provider.GetRequiredService<IOptions<AppSettings>>().Value);

	}

	private static string FormatEnvironmentVariable(string variable) {

		return variable.Replace("-", "_").Replace(".", "_").ToUpper();

	}

	private static string GetConnectionStringFromEnvironment(this IConfiguration configuration, string value, string name) {

		return configuration.GetValue($"ConnectionStrings:{name}", value);

	}

	private static string GetValueFromEnvironment(this IConfiguration configuration, string value, string variable) {

		variable = FormatEnvironmentVariable(variable);

		return configuration.GetValue(variable, value);

	}

	private static int GetValueFromEnvironment(this IConfiguration configuration, int value, string variable) {

		variable = FormatEnvironmentVariable(variable);

		return configuration.GetValue(variable, value);

	}

}
