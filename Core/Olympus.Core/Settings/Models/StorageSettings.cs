namespace Olympus.Core.Settings;

public class StorageSettings {

	public const string SectionName = "Storage";

	public const string ServiceName = "Storage";

	public string ConnectionString { get; set; } = string.Empty;

	public string Host { get; set; } = string.Empty;

	public string AccessKey { get; set; } = string.Empty;

	public string SecretKey { get; set; } = string.Empty;

	public string Region { get; set; } = string.Empty;

	public bool UseSSL { get; set; }

	public int Port { get; set; }

	public Uri? Uri { get; set; }

}
