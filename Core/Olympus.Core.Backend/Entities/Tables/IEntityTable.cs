namespace Olympus.Core.Backend.Entities;

public interface IEntityTable {

	public void Apply(ModelBuilder builder);

}

public interface IEntityTable<TEntity> : IEntityTable, IEntityTypeConfiguration<TEntity> where TEntity : class { }
