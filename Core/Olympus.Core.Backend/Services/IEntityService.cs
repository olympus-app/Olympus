namespace Olympus.Core.Backend.Services;

public interface IEntityService { }

public interface IEntityService<TEntity> : IEntityService where TEntity : class, IEntity {

	public Task<IQueryable<TEntity>> QueryAsync();

	public Task<IQueryable<TEntity>> QueryAsync(Guid id);

	public Task<IEnumerable<TEntity>> GetAsync();

	public Task<TEntity> GetAsync(Guid id);

	public Task<TEntity> CreateAsync(TEntity entity);

	public Task<TEntity> UpdateAsync(TEntity entity);

	public Task DeleteAsync(TEntity entity);

}
