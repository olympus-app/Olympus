using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

namespace Olympus.Api.Identity;

public static class DataProtectionRegistrator {

	public static void AddDataProtectionServices(this WebApplicationBuilder builder) {

		var connectionString = builder.Configuration.GetConnectionString(CacheSettings.ServiceName) ?? throw new InvalidOperationException(nameof(DataProtectionRegistrator));

		var dataProtectionConnection = ConnectionMultiplexer.Connect(connectionString);

		builder.Services.AddDataProtection().PersistKeysToStackExchangeRedis(dataProtectionConnection).SetApplicationName(AppSettings.AppBaseName);

	}

}
