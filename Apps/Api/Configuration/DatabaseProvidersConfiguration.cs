using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Olympus.Api;

public static class DatabaseProvidersConfiguration {

	private const string MigrationsAssemblyName = "Olympus.Api.Host";
	private const string MigrationsTableName = "Migrations";

	private static void AddServices(this IServiceCollection services) {

		services.AddScoped<DatabaseSaveChangesInterceptor>();

	}

	public static void AddInMemoryDatabase(this WebApplicationBuilder builder, string databaseName = "Database") {

		builder.Services.AddServices();
		builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
			options.AddInterceptors(services.GetRequiredService<DatabaseSaveChangesInterceptor>());
			options.UseInMemoryDatabase(databaseName);
		});

	}

	public static void AddSQLite(this WebApplicationBuilder builder, string connectionStringName = "SQLite") {

		builder.Services.AddServices();
		builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
			options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
			options.AddInterceptors(services.GetRequiredService<DatabaseSaveChangesInterceptor>());
			options.UseSqlite(builder.Configuration.GetConnectionString(connectionStringName), options => {
				options.MigrationsAssembly(MigrationsAssemblyName);
				options.MigrationsHistoryTable(MigrationsTableName);
				options.CommandTimeout(30);
			});
		});

	}

	public static void AddPostgreSql(this WebApplicationBuilder builder, string connectionStringName = "PostgreSQL") {

		builder.Services.AddServices();
		builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
			options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
			options.AddInterceptors(services.GetRequiredService<DatabaseSaveChangesInterceptor>());
			options.UseNpgsql(builder.Configuration.GetConnectionString(connectionStringName), options => {
				options.MigrationsAssembly(MigrationsAssemblyName);
				options.MigrationsHistoryTable(MigrationsTableName);
				options.EnableRetryOnFailure();
				options.CommandTimeout(30);
			});
		});

	}

	public static void AddAzureSql(this WebApplicationBuilder builder, string connectionStringName = "AzureSql") {

		builder.Services.AddServices();
		builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
			options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
			options.AddInterceptors(services.GetRequiredService<DatabaseSaveChangesInterceptor>());
			options.UseAzureSql(builder.Configuration.GetConnectionString(connectionStringName), options => {
				options.MigrationsAssembly(MigrationsAssemblyName);
				options.MigrationsHistoryTable(MigrationsTableName);
				options.EnableRetryOnFailure();
				options.CommandTimeout(30);
			});
		});

	}

	public static void AddSqlServer(this WebApplicationBuilder builder, string connectionStringName = "SqlServer") {

		builder.Services.AddServices();
		builder.Services.AddDbContext<EntitiesDatabase>((services, options) => {
			options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
			options.AddInterceptors(services.GetRequiredService<DatabaseSaveChangesInterceptor>());
			options.UseSqlServer(builder.Configuration.GetConnectionString(connectionStringName), options => {
				options.MigrationsAssembly(MigrationsAssemblyName);
				options.MigrationsHistoryTable(MigrationsTableName);
				options.EnableRetryOnFailure();
				options.CommandTimeout(30);
			});
		});

	}

}
