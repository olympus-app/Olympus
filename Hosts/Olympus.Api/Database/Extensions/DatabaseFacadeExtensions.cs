using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Olympus.Api.Database;

public static class DatabaseFacadeExtensions {

	extension(DatabaseFacade database) {

		public bool IsInMemory() => database?.ProviderName?.Contains("InMemory", StringComparison.OrdinalIgnoreCase) ?? false;

		public bool IsSqlServer() => database?.ProviderName?.Contains("SQLServer", StringComparison.OrdinalIgnoreCase) ?? false;

		public bool IsPostgreSql() => database?.ProviderName?.Contains("PostgreSQL", StringComparison.OrdinalIgnoreCase) ?? database?.ProviderName?.Contains("Npgsql", StringComparison.OrdinalIgnoreCase) ?? false;

	}

}
