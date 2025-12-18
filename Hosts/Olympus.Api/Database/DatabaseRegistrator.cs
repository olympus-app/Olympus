using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Olympus.Api.Database;

public static class DatabaseRegistrator {

	public static void AddDatabaseServices(this WebApplicationBuilder builder) {

		var connectionString = builder.Configuration.GetConnectionString(DatabaseSettings.DatabaseName);
		var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString).EnableDynamicJson();
		dataSourceBuilder.ConnectionStringBuilder.IncludeErrorDetail = builder.Environment.IsDevelopment();

		var dataSource = dataSourceBuilder.Build();

		builder.Services.AddSingleton(dataSource);

		builder.Services.AddSingleton<AuditInterceptor>();

		builder.Services.AddScoped<IDatabaseContext>(static provider => provider.GetRequiredService<IDbContextFactory<DatabaseContext>>().CreateDbContext());

		builder.Services.AddPooledDbContextFactory<DatabaseContext>((services, options) => {
			var model = services.GetService<IModel>();
			if (model is not null) options.UseModel(model);
			options.UseExceptionProcessor();
			options.AddInterceptors(services.GetRequiredService<AuditInterceptor>());
			options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
			options.ConfigureWarnings(static warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
			options.UseNpgsql(dataSource, static options => {
				options.MigrationsAssembly(DatabaseSettings.MigrationsAssemblyName);
				options.MigrationsHistoryTable(DatabaseSettings.MigrationsTableName, DatabaseSettings.MigrationsSchemaName);
				options.EnableRetryOnFailure();
			});
		});

		builder.EnrichNpgsqlDbContext<DatabaseContext>();

		builder.AddRedisDistributedCache(CacheSettings.ServiceName);

		builder.Services.AddHybridCache();

		builder.Services.AddSingleton<IEFCacheServiceProvider, DatabaseCache>();

		builder.Services.AddEFSecondLevelCache(static options => {
			options.UseCustomCacheProvider<DatabaseCache>(CacheExpirationMode.Sliding, 1.Hours());
			options.UseDbCallsIfCachingProviderIsDown(5.Minutes());
			options.ConfigureLogging(false);
		});

	}

	public static void MigrateDatabase(this WebApplication app) {

		using var scope = app.Services.CreateScope();
		var database = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

		database.Database.Migrate();

	}

}
