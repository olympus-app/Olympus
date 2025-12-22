namespace Olympus.Core.Settings;

public class CacheSettings {

	public const string SectionName = "Cache";

	public const string ServiceName = "Redis";

	public string Host { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public int Port { get; set; }

	public string ConnectionString {
		get => field ?? $"{Host}:{Port},password={Password}";
		set => field = value;
	}

}
