namespace Olympus.Core.Settings;

public class CacheSettings {

	public const string SectionName = "Cache";

	public const string ServiceName = "Cache";

	public string ConnectionString { get; set; } = string.Empty;

	public string Host { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public int Port { get; set; }

	public Uri? Uri { get; set; }

}
