namespace Olympus.Core.Backend.Entities;

public abstract class EntityDeleteEndpoint<TEntity, TDeleteRequest>(IEntityService<TEntity> service) : EntityEndpoint<TEntity>.WithRequest<TDeleteRequest>(service) where TEntity : class, IEntity where TDeleteRequest : class, IEntityDeleteRequest {

	public override async Task<Void> HandleAsync(TDeleteRequest request, CancellationToken cancellationToken) {

		var entity = await Service.ReadAsync(request.Id, true, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		if (ConflictCheck(entity, request.ETag)) return await Send.ConflictAsync(cancellationToken);

		entity = await Service.DeleteAsync(entity, request.Force, cancellationToken);

		return await Send.DeletedAsync(entity, cancellationToken);

	}

}
