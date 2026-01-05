namespace Olympus.Core.Settings;

public class DatabaseSettings : Settings {

	public const string ServiceName = "Postgres";

	public const string DatabaseName = "Database";

	public const string MigrationsTableName = "Migrations";

	public const string MigrationsSchemaName = "Internal";

	public string Host { get; set; } = string.Empty;

	public string Username { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public int Port { get; set; }

	public string ConnectionString { get => field ?? $"Host={Host};Port={Port};Username={Username};Password={Password};Database={DatabaseName}"; set; }

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Database", this);

		Host = configuration.GetValueFromEnvironment($"{DatabaseName}_{nameof(Host)}", Host);
		Port = configuration.GetValueFromEnvironment($"{DatabaseName}_{nameof(Port)}", Port);
		Username = configuration.GetValueFromEnvironment($"{DatabaseName}_{nameof(Username)}", Username);
		Password = configuration.GetValueFromEnvironment($"{DatabaseName}_{nameof(Password)}", Password);
		ConnectionString = configuration.GetConnectionString(DatabaseName, ConnectionString);

	}

}
