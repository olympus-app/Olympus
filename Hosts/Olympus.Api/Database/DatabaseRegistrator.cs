using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Olympus.Api.Database;

public static class DatabaseRegistrator {

	public static void AddDatabaseServices(this WebApplicationBuilder builder, AppHostInfo info) {

		var connectionString = builder.Configuration.GetConnectionString(DatabaseSettings.DatabaseName) ?? throw new InvalidOperationException(nameof(DatabaseRegistrator));

		var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString).EnableDynamicJson();

		dataSourceBuilder.ConnectionStringBuilder.IncludeErrorDetail = builder.Environment.IsDevelopment();

		var dataSource = dataSourceBuilder.Build();

		builder.Services.AddSingleton(dataSource);

		builder.Services.AddSingleton<AuditInterceptor>();

		builder.Services.AddSingleton<IEFCacheServiceProvider, CacheProvider>();

		builder.Services.AddScoped<IEntityDatabase>(static provider => provider.GetRequiredService<IDbContextFactory<EntityDatabase>>().CreateDbContext());

		builder.Services.AddPooledDbContextFactory<EntityDatabase>((services, options) => {
			var model = services.GetService<IModel>();
			if (model is not null) options.UseModel(model);
			options.UseExceptionProcessor();
			options.AddInterceptors(services.GetRequiredService<AuditInterceptor>());
			options.AddInterceptors(services.GetRequiredService<SecondLevelCacheInterceptor>());
			options.ConfigureWarnings(static warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
			options.UseNpgsql(dataSource, options => {
				options.MigrationsAssembly(info.Assembly);
				options.MigrationsHistoryTable(DatabaseSettings.MigrationsTableName, DatabaseSettings.MigrationsSchemaName);
				options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
			});
		});

		builder.EnrichNpgsqlDbContext<EntityDatabase>();

		builder.Services.AddEFSecondLevelCache(static options => {
			options.UseCustomCacheProvider<CacheProvider>(CacheExpirationMode.Sliding, 1.Hours());
			options.UseDbCallsIfCachingProviderIsDown(5.Minutes());
			options.ConfigureLogging(false);
		});

	}

	public static void UpdateDatabase(this WebApplication app) {

		using var scope = app.Services.CreateScope();
		var database = scope.ServiceProvider.GetRequiredService<EntityDatabase>();

		database.Database.Migrate();

	}

}
