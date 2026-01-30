namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithStorageDeleteEndpoint<TEntityWithStorage, TStorageEntity, TEntityWithStorageDeleteRequest>(IEntityWithStorageService<TEntityWithStorage, TStorageEntity> service) : EntityEndpoint<TEntityWithStorage>.WithRequest<TEntityWithStorageDeleteRequest>(service) where TEntityWithStorage : class, IEntityWithStorage<TStorageEntity>, new() where TStorageEntity : class, IStorageEntity, new() where TEntityWithStorageDeleteRequest : class, IEntityWithStorageDeleteRequest {

	public override async Task<Void> HandleAsync(TEntityWithStorageDeleteRequest request, CancellationToken cancellationToken) {

		var entity = await Service.ReadAsync(request.Id, false, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		if (ConflictCheck(entity, request.ETag)) return await Send.ConflictAsync(cancellationToken);

		var stream = await service.DeleteAsync(entity, cancellationToken);

		return await Send.DeletedAsync(entity, cancellationToken);

	}

}
