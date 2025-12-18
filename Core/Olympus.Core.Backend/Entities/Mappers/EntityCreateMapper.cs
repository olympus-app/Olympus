namespace Olympus.Core.Backend.Entities;

public abstract class EntityCreateMapper<TEntity, TCreateRequest, TReadResponse> : IEntityCreateMapper<TEntity, TCreateRequest, TReadResponse> where TEntity : class, IEntity where TCreateRequest : class, IEntityCreateRequest where TReadResponse : class, IEntityReadResponse {

	protected abstract TEntity Map(TCreateRequest request);

	protected abstract TReadResponse Map(TEntity entity);

	public virtual TEntity ToEntity(TCreateRequest request) => Map(request);

	public virtual TReadResponse FromEntity(TEntity entity) => Map(entity);

	public virtual TEntity UpdateEntity(TCreateRequest request, TEntity entity) => throw new NotSupportedException();

	public virtual Task<TEntity> ToEntityAsync(TCreateRequest request, CancellationToken cancellationToken) => Task.FromResult(Map(request));

	public virtual Task<TReadResponse> FromEntityAsync(TEntity entity, CancellationToken cancellationToken) => Task.FromResult(Map(entity));

	public virtual Task<TEntity> UpdateEntityAsync(TCreateRequest request, TEntity entity, CancellationToken cancellationToken) => throw new NotSupportedException();

}
