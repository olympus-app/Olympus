namespace Olympus.Core.Backend.Entities;

public abstract class EntityEndpoint<TEntity>(IEntityService<TEntity> service) : Endpoint where TEntity : class, IEntity {

	public IEntityService<TEntity> Service { get; set; } = service;

	public bool ConflictCheck(TEntity entity, string? etag) => !EntityTag.IfMatch(etag, entity.ETag);

	public new abstract class WithRequest<TRequest>(IEntityService<TEntity> service) : Endpoint.WithRequest<TRequest> where TRequest : class, IEntityRequest {

		public IEntityService<TEntity> Service { get; set; } = service;

		public bool ConflictCheck(TEntity entity, string? etag) => !EntityTag.IfMatch(etag, entity.ETag);

		public new abstract class WithResponse<TResponse>(IEntityService<TEntity> service) : Endpoint.WithRequest<TRequest>.WithResponse<TResponse> where TResponse : class, IEntityResponse {

			public IEntityService<TEntity> Service { get; set; } = service;

			public bool ConflictCheck(TEntity entity, string? etag) => !EntityTag.IfMatch(etag, entity.ETag);

		}

	}

	public new abstract class WithResponse<TResponse>(IEntityService<TEntity> service) : Endpoint.WithResponse<TResponse> where TResponse : class, IEntityResponse {

		public IEntityService<TEntity> Service { get; set; } = service;

		public bool ConflictCheck(TEntity entity, string? etag) => !EntityTag.IfMatch(etag, entity.ETag);

	}

}
