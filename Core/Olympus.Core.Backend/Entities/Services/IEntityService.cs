namespace Olympus.Core.Backend.Entities;

public interface IEntityService<TEntity> : IScopedService<IEntityService<TEntity>> where TEntity : class, IEntity {

	public IQueryable<TEntity> Query(bool tracking = false);

	public IQueryable<TEntity> Query(Guid id, bool tracking = false);

	public Task<List<TEntity>> ListAsync(bool tracking = false, CancellationToken cancellationToken = default);

	public Task<TEntity?> ReadAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default);

	public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

	public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

	public Task<TEntity?> DeleteAsync(TEntity entity, bool force, CancellationToken cancellationToken = default);

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
