namespace Olympus.Core.Settings;

public class StorageSettings : Settings {

	public const string ServiceName = "Minio";

	public string Protocol { get; set; } = "http";

	public string Host { get; set; } = string.Empty;

	public string AccessKey { get; set; } = string.Empty;

	public string SecretKey { get; set; } = string.Empty;

	public string Region { get; set; } = string.Empty;

	public int Port { get; set; }

	public bool UseSSL { get; set; }

	public string ConnectionString { get => field ?? $"Endpoint={Protocol}://{Host}:{Port};AccessKey={AccessKey};SecretKey={SecretKey}"; set; }

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Storage", this);

		Host = configuration.GetValueFromEnvironment($"{ServiceName}_{nameof(Host)}", Host);
		Port = configuration.GetValueFromEnvironment($"{ServiceName}_{nameof(Port)}", Port);
		AccessKey = configuration.GetValueFromEnvironment($"{ServiceName}_{nameof(AccessKey)}", AccessKey);
		SecretKey = configuration.GetValueFromEnvironment($"{ServiceName}_{nameof(SecretKey)}", SecretKey);
		ConnectionString = configuration.GetConnectionString(ServiceName, ConnectionString);

	}

}
