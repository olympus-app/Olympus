namespace Olympus.Core.Settings;

public class DatabaseSettings {

	public const string SectionName = "Database";

	public const string ServiceName = "Database";

	public const string DatabaseName = "Olympus";

	public const string MigrationsTableName = "Migrations";

	public const string MigrationsSchemaName = "Internal";

	public const string MigrationsAssemblyName = "Olympus.Api.Host";

	public string ConnectionString { get; set; } = string.Empty;

	public string Host { get; set; } = string.Empty;

	public string Username { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public int Port { get; set; }

	public Uri? Uri { get; set; }

}
