namespace Olympus.Core.Backend.Entities;

public abstract class EntityCreateEndpoint<TEntity, TCreateRequest, TReadResponse, TMapper> : EntityEndpoint<TEntity, TCreateRequest, TReadResponse, TMapper> where TEntity : class, IEntity where TCreateRequest : class, IEntityCreateRequest where TReadResponse : class, IEntityReadResponse where TMapper : class, IEntityCreateMapper<TEntity, TCreateRequest, TReadResponse> {

	protected virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default) {

		Database.Set<TEntity>().Add(entity);

		await Database.SaveChangesAsync(cancellationToken);

		entity.CreatedBy = User.CurrentUser;
		entity.UpdatedBy ??= entity.CreatedBy;

		return entity;

	}

	public override async Task<Void> HandleAsync(TCreateRequest request, CancellationToken cancellationToken) {

		var entity = Map.ToEntity(request);

		entity = await CreateAsync(entity, cancellationToken);

		var response = Map.FromEntity(entity);

		return await Send.CreatedAsync(response, cancellationToken);

	}

}
