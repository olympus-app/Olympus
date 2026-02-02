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

	public bool IsAdded<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State == EntityState.Added;

	public bool IsModified<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State == EntityState.Modified;

	public bool IsDeleted<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State == EntityState.Deleted;

	public bool IsUnchanged<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State == EntityState.Unchanged;

	public bool IsDetached<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State == EntityState.Detached;

}
