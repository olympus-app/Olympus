namespace Olympus.Core.Backend.Tables;

public interface IEntityTable {

	public void Apply(ModelBuilder builder);

}

public interface IEntityTable<TEntity> : IEntityTable, IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity { }
