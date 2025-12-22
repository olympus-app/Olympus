namespace Olympus.Core.Backend.Entities;

public abstract class EntityQueryMapper<TEntity, TQueryResponse> : IEntityQueryMapper<TEntity, TQueryResponse> where TEntity : class, IEntity where TQueryResponse : class, IEntityQueryResponse {

	protected abstract IQueryable<TQueryResponse> Map(IQueryable<TEntity> entities);

	public virtual IQueryable<TQueryResponse> FromEntity(IQueryable<TEntity> entities) => Map(entities);

	public virtual Task<IQueryable<TQueryResponse>> FromEntityAsync(IQueryable<TEntity> entities, CancellationToken cancellationToken) => Task.FromResult(Map(entities));

}
