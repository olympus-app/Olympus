using Microsoft.Extensions.Configuration;

namespace Olympus.Api.Database;

public static class DatabaseProviderRegistrator {

	private const string MigrationsTableName = "Migrations";
	private const string MigrationsAssemblyName = "Olympus.Api.Host";
	private const string DatabaseDefaultFileName = "OlympusDatabase";
	private const string DatabaseProviderSettingsPath = "Database:Provider";
	private const string DatabaseConnectionSettingsPath = "Database:Connection";

	public static void AddDatabaseServices(this WebApplicationBuilder builder, DatabaseProvider? databaseProvider = null, string? connectionString = null) {

		databaseProvider ??= builder.Configuration.GetValue<DatabaseProvider>(DatabaseProviderSettingsPath);
		connectionString ??= builder.Configuration.GetValue<string>(DatabaseConnectionSettingsPath);

		builder.Services.AddScoped<AuditInterceptor>();

		switch (databaseProvider) {

			case DatabaseProvider.SQLite:

				builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
					options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
					options.AddInterceptors(services.GetRequiredService<AuditInterceptor>());
					options.UseSqlite(connectionString, options => {
						options.MigrationsAssembly(MigrationsAssemblyName);
						options.MigrationsHistoryTable(MigrationsTableName);
						options.CommandTimeout(30);
					});
				}); break;

			case DatabaseProvider.PostgreSql:

				builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
					options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
					options.AddInterceptors(services.GetRequiredService<AuditInterceptor>());
					options.UseNpgsql(connectionString, options => {
						options.MigrationsAssembly(MigrationsAssemblyName);
						options.MigrationsHistoryTable(MigrationsTableName);
						options.EnableRetryOnFailure();
						options.CommandTimeout(30);
					});
				}); break;

			case DatabaseProvider.AzureSql:

				builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
					options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
					options.AddInterceptors(services.GetRequiredService<AuditInterceptor>());
					options.UseAzureSql(connectionString, options => {
						options.MigrationsAssembly(MigrationsAssemblyName);
						options.MigrationsHistoryTable(MigrationsTableName);
						options.EnableRetryOnFailure();
						options.CommandTimeout(30);
					});
				}); break;

			case DatabaseProvider.SqlServer:

				builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
					options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
					options.AddInterceptors(services.GetRequiredService<AuditInterceptor>());
					options.UseSqlServer(connectionString, options => {
						options.MigrationsAssembly(MigrationsAssemblyName);
						options.MigrationsHistoryTable(MigrationsTableName);
						options.EnableRetryOnFailure();
						options.CommandTimeout(30);
					});
				}); break;

			default:

				builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
					options.AddInterceptors(services.GetRequiredService<AuditInterceptor>());
					options.UseInMemoryDatabase(connectionString ?? DatabaseDefaultFileName);
				}); break;

		}

	}

}
