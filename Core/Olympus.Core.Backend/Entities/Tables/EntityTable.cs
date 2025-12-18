namespace Olympus.Core.Backend.Entities;

public abstract class EntityTable<TEntity> : IEntityTable<TEntity> where TEntity : class {

	private static readonly DateTimeOffset SeedDate = new(2025, 1, 1, 0, 0, 0, TimeSpan.Zero);

	public abstract void Configure(EntityTypeBuilder<TEntity> builder);

	public void Apply(ModelBuilder builder) {

		builder.ApplyConfiguration(this);

	}

	protected static T PrepareSeed<T>(T entity, bool active = true, bool hidden = false, bool locked = true, bool system = true) where T : IEntity {

		entity.RowVersion = Guid.Empty;

		entity.CreatedById ??= AppUsers.System.Id;
		entity.CreatedAt ??= SeedDate;

		entity.UpdatedById ??= AppUsers.System.Id;
		entity.UpdatedAt ??= SeedDate;

		entity.IsDeleted = false;
		entity.IsActive = active;
		entity.IsHidden = hidden;
		entity.IsLocked = locked;
		entity.IsSystem = system;

		return entity;

	}

}
