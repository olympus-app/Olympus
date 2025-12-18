namespace Olympus.Core.Backend.Entities;

public abstract class EntityUpdateMapper<TEntity, TUpdateRequest, TReadResponse> : IEntityUpdateMapper<TEntity, TUpdateRequest, TReadResponse> where TEntity : class, IEntity where TUpdateRequest : class, IEntityUpdateRequest where TReadResponse : class, IEntityReadResponse {

	protected abstract void Map(TUpdateRequest request, TEntity entity);

	protected abstract TReadResponse Map(TEntity entity);

	public virtual TEntity ToEntity(TUpdateRequest request) => throw new NotSupportedException();

	public virtual TReadResponse FromEntity(TEntity entity) => Map(entity);

	public virtual TEntity UpdateEntity(TUpdateRequest request, TEntity entity) {

		Map(request, entity);

		return entity;

	}

	public virtual Task<TEntity> ToEntityAsync(TUpdateRequest request, CancellationToken cancellationToken) => throw new NotSupportedException();

	public virtual Task<TReadResponse> FromEntityAsync(TEntity entity, CancellationToken cancellationToken) => Task.FromResult(Map(entity));

	public virtual Task<TEntity> UpdateEntityAsync(TUpdateRequest request, TEntity entity, CancellationToken cancellationToken) {

		Map(request, entity);

		return Task.FromResult(entity);

	}

}
