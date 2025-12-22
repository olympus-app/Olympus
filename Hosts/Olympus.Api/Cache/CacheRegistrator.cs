using Microsoft.Extensions.Hosting;

namespace Olympus.Api.Cache;

public static class CacheRegistrator {

	public static void AddCacheServices(this WebApplicationBuilder builder) {

		builder.Services.AddHybridCache();

		builder.AddRedisDistributedCache(CacheSettings.ServiceName);

	}

}
