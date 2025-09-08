namespace Olympus.Core.Backend;

public interface IEntityTable {

	public void Apply(ModelBuilder builder);

}

public interface IEntityTable<TEntity> : IEntityTable, IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity { }
