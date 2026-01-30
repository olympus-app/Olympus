using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Olympus.Api.Database;

public class DatabaseService(DbContextOptions<DatabaseService> options, IEnumerable<IEntityTable> tables, CultureSettings settings) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken, UserPasskey>(options), IDatabaseService {

	protected override void OnModelCreating(ModelBuilder builder) {

		base.OnModelCreating(builder);

		builder.ApplyEntityConfigurations(Database, tables, settings);

		builder.ApplyColumnsConventions();

		builder.ApplyColumnsOrder();

	}

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {

		configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();

		configurationBuilder.Properties<DateTime?>().HaveConversion<UtcDateTimeConverter>();

		configurationBuilder.Properties<DateTimeOffset>().HaveConversion<UtcDateTimeOffsetConverter>();

		configurationBuilder.Properties<DateTimeOffset?>().HaveConversion<UtcDateTimeOffsetConverter>();

	}

}
