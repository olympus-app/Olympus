namespace Olympus.Core.Settings;

public class CacheSettings : Settings {

	public const string ServiceName = "Redis";

	public string Host { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public int Port { get; set; }

	public string ConnectionString { get => field ?? $"{Host}:{Port},password={Password}"; set; }

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Cache", this);

		Host = configuration.GetValueFromEnvironment($"{ServiceName}_{nameof(Host)}", Host);
		Port = configuration.GetValueFromEnvironment($"{ServiceName}_{nameof(Port)}", Port);
		Password = configuration.GetValueFromEnvironment($"{ServiceName}_{nameof(Password)}", Password);
		ConnectionString = configuration.GetConnectionString(ServiceName, ConnectionString);

	}

}
