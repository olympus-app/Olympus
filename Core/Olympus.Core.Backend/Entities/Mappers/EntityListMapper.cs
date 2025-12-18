namespace Olympus.Core.Backend.Entities;

public abstract class EntityListMapper<TEntity, TListResponse> : IEntityListMapper<TEntity, TListResponse> where TEntity : class, IEntity where TListResponse : class, IEntityListResponse {

	protected abstract IQueryable<TListResponse> Map(IQueryable<TEntity> entities);

	public virtual IQueryable<TListResponse> FromEntity(IQueryable<TEntity> entities) => Map(entities);

	public virtual Task<IQueryable<TListResponse>> FromEntityAsync(IQueryable<TEntity> entities, CancellationToken cancellationToken) => Task.FromResult(Map(entities));

}
