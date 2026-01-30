namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithStorageLinkEndpoint<TEntityWithStorage, TStorageEntity, TEntityWithStorageLinkRequest>(IEntityWithStorageService<TEntityWithStorage, TStorageEntity> service) : EntityEndpoint<TEntityWithStorage>.WithRequest<TEntityWithStorageLinkRequest>(service) where TEntityWithStorage : class, IEntityWithStorage<TStorageEntity>, new() where TStorageEntity : class, IStorageEntity, new() where TEntityWithStorageLinkRequest : class, IEntityWithStorageLinkRequest {

	public override async Task<Void> HandleAsync(TEntityWithStorageLinkRequest request, CancellationToken cancellationToken) {

		var entity = await Service.ReadAsync(request.Id, false, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		var link = await service.LinkAsync(entity, cancellationToken);

		return await Send.StringAsync(link, cancellationToken);

	}

}
