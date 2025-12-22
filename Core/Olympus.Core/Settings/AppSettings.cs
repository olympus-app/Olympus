using System.Reflection;

namespace Olympus.Core.Settings;

public class AppSettings {

	public const string SectionName = "";

	public const string AppBaseName = "Olympus";

	public const string ApiBaseName = "Olympus API";

	public const string WebBaseName = "Olympus Web";

	public const string UtcDateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";

	public static class EmbeddedSettings {

		public const string Global = "Settings.Global.json";

		public const string Common = "Settings.Common.json";

		public const string Development = "Settings.Development.json";

		public const string Production = "Settings.Production.json";

		public static string GetByType(AppSettingsType type) => Path.Combine("Settings", $"{type}.json");

	}

	public string AppName { get; set; } = AppBaseName;

	public string ApiName { get; set; } = ApiBaseName;

	public string WebName { get; set; } = WebBaseName;

	public HostSettings Host { get; } = new();

	public AdminSettings Admin { get; } = new();

	public AuthorSettings Author { get; } = new();

	public CompanySettings Company { get; } = new();

	public LicenseSettings License { get; } = new();

	public CultureSettings Culture { get; } = new();

	public IdentitySettings Identity { get; } = new();

	public DatabaseSettings Database { get; } = new();

	public StorageSettings Storage { get; } = new();

	public CacheSettings Cache { get; } = new();

	public Dictionary<string, ModuleSettings> Modules { get; } = [];

	public static readonly string Copyright;

	public static readonly string Version;

	public static readonly string VersionShort;

	public static readonly int BuildNumber;

	public static readonly int BuildNumberForced;

	static AppSettings() {

		var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

		Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty;

		Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? assembly.GetName().Version?.ToString() ?? "0.0.0.0-unknown";

		VersionShort = assembly.GetCustomAttribute<AssemblyVersionAttribute>()?.Version ?? assembly.GetName().Version?.ToString() ?? "0.0.0.0";

		foreach (var meta in assembly.GetCustomAttributes<AssemblyMetadataAttribute>()) {

			if (meta.Key == "BuildNumber" && int.TryParse(meta.Value, out var build)) {

				BuildNumber = build;

			} else if (meta.Key == "BuildNumberForced" && int.TryParse(meta.Value, out var forced)) {

				BuildNumberForced = forced;

			}

		}

	}

}
