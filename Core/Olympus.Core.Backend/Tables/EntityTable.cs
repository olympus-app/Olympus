namespace Olympus.Core.Backend.Tables;

public abstract class EntityTable<TEntity> : IEntityTable<TEntity> where TEntity : class, IEntity {

	public virtual void Configure(EntityTypeBuilder<TEntity> builder) {

		builder.ToTable(typeof(TEntity).Name.Pluralize());

		builder.HasKey(e => e.Id);
		builder.HasOne(e => e.CreatedBy).WithMany().HasForeignKey(e => e.CreatedById).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(e => e.UpdatedBy).WithMany().HasForeignKey(e => e.UpdatedById).OnDelete(DeleteBehavior.NoAction);
		builder.Property(e => e.Version).IsConcurrencyToken();
		builder.HasIndex(e => e.Version);

	}

	public void Apply(ModelBuilder builder) {

		builder.ApplyConfiguration(this);

	}

}
