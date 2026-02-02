namespace Olympus.Core.Backend.Entities;

public abstract class EntityEndpoint<TEntity>(IEntityService<TEntity> service) : Endpoint where TEntity : class, IEntity {

	protected IEntityService<TEntity> Service { get; set; } = service;

	protected bool ConflictCheck(TEntity entity) => ConflictCheck(entity.ETag);

	protected bool NotModifiedCheck(TEntity entity) => NotModifiedCheck(entity.ETag);

	public new abstract class WithRequest<TRequest>(IEntityService<TEntity> service) : Endpoint.WithRequest<TRequest> where TRequest : class, IEntityRequest {

		protected IEntityService<TEntity> Service { get; set; } = service;

		protected bool ConflictCheck(TEntity entity) => ConflictCheck(entity.ETag);

		protected bool NotModifiedCheck(TEntity entity) => NotModifiedCheck(entity.ETag);

		public new abstract class WithResponse<TResponse>(IEntityService<TEntity> service) : Endpoint.WithRequest<TRequest>.WithResponse<TResponse> where TResponse : class, IEntityResponse {

			protected IEntityService<TEntity> Service { get; set; } = service;

			protected bool ConflictCheck(TEntity entity) => ConflictCheck(entity.ETag);

			protected bool NotModifiedCheck(TEntity entity) => NotModifiedCheck(entity.ETag);

		}

	}

	public new abstract class WithResponse<TResponse>(IEntityService<TEntity> service) : Endpoint.WithResponse<TResponse> where TResponse : class, IEntityResponse {

		protected IEntityService<TEntity> Service { get; set; } = service;

		protected bool ConflictCheck(TEntity entity) => ConflictCheck(entity.ETag);

		protected bool NotModifiedCheck(TEntity entity) => NotModifiedCheck(entity.ETag);

	}

}
