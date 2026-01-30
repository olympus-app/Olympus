namespace Olympus.Api.Queries;

#pragma warning disable IDE0060

public static class QueriesRegistrator {

	public static void AddQueryingServices(this WebApplicationBuilder builder) {

		GridifyGlobalConfiguration.AllowNullSearch = true;
		GridifyGlobalConfiguration.AvoidNullReference = true;
		GridifyGlobalConfiguration.CaseInsensitiveFiltering = true;
		GridifyGlobalConfiguration.CaseSensitiveMapper = false;
		GridifyGlobalConfiguration.DefaultDateTimeKind = DateTimeKind.Utc;
		GridifyGlobalConfiguration.DefaultPageSize = 10;
		GridifyGlobalConfiguration.DisableNullChecks = false;
		GridifyGlobalConfiguration.EntityFrameworkCompatibilityLayer = true;
		GridifyGlobalConfiguration.IgnoreNotMappedFields = false;

	}

}

#pragma warning restore IDE0060
