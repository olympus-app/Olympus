using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Olympus.Api.Database;

public class DatabaseContext(AppSettings settings, DbContextOptions<DatabaseContext> options, IEnumerable<IEntityTable> tables) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken, UserPasskey>(options), IDatabaseContext {

	protected override void OnModelCreating(ModelBuilder builder) {

		base.OnModelCreating(builder);

		builder.ApplyConfigurations(Database, tables, settings);

	}

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {

		configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();

		configurationBuilder.Properties<DateTime?>().HaveConversion<UtcDateTimeConverter>();

		configurationBuilder.Properties<DateTimeOffset>().HaveConversion<UtcDateTimeOffsetConverter>();

		configurationBuilder.Properties<DateTimeOffset?>().HaveConversion<UtcDateTimeOffsetConverter>();

	}

}
