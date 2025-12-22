namespace Olympus.Core.Settings;

public class DatabaseSettings {

	public const string SectionName = "Database";

	public const string ServiceName = "Postgres";

	public const string DatabaseName = "Database";

	public const string MigrationsTableName = "Migrations";

	public const string MigrationsSchemaName = "Internal";

	public const string MigrationsAssemblyName = "Olympus.Api.Host";

	public string Host { get; set; } = string.Empty;

	public string Username { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public int Port { get; set; }

	public string ConnectionString {
		get => field ?? $"Host={Host};Port={Port};Username={Username};Password={Password};Database={DatabaseName}";
		set => field = value;
	}

}
