namespace Olympus.Core.Settings;

public class StorageSettings {

	public const string SectionName = "Storage";

	public const string ServiceName = "Minio";

	public string Protocol { get; set; } = "http";

	public string Host { get; set; } = string.Empty;

	public string AccessKey { get; set; } = string.Empty;

	public string SecretKey { get; set; } = string.Empty;

	public string Region { get; set; } = string.Empty;

	public int Port { get; set; }

	public bool UseSSL { get; set; }

	public string ConnectionString {
		get => field ?? $"Endpoint={Protocol}://{Host}:{Port};AccessKey={AccessKey};SecretKey={SecretKey}";
		set => field = value;
	}

}
