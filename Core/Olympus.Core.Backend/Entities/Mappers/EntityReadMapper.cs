namespace Olympus.Core.Backend.Entities;

public abstract class EntityReadMapper<TEntity, TReadResponse> : IEntityReadMapper<TEntity, TReadResponse> where TEntity : class, IEntity where TReadResponse : class, IEntityReadResponse {

	protected abstract IQueryable<TReadResponse> Map(IQueryable<TEntity> entity);

	public virtual IQueryable<TReadResponse> FromEntity(IQueryable<TEntity> entity) => Map(entity);

	public virtual Task<IQueryable<TReadResponse>> FromEntityAsync(IQueryable<TEntity> entity, CancellationToken cancellationToken) => Task.FromResult(Map(entity));

}
