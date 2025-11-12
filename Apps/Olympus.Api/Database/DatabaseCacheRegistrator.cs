namespace Olympus.Api.Database;

internal static class DatabaseCacheRegistrator {

	private const string CachePrefix = "EF_";

	internal static void AddDatabaseCacheServices(this WebApplicationBuilder builder) {

		builder.Services.AddEFSecondLevelCache(options => {
			options.UseMemoryCacheProvider(CacheExpirationMode.Absolute, 24.Hours());
			options.UseDbCallsIfCachingProviderIsDown(3.Minutes());
			options.UseCacheKeyPrefix(CachePrefix);
			options.ConfigureLogging(false);
		});

	}

}
