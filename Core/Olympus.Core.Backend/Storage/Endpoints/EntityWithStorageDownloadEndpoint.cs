namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithStorageDownloadEndpoint<TEntityWithStorage, TStorageEntity, TEntityWithStorageDownloadRequest>(IEntityWithStorageService<TEntityWithStorage, TStorageEntity> service) : EntityEndpoint<TEntityWithStorage>.WithRequest<TEntityWithStorageDownloadRequest>(service) where TEntityWithStorage : class, IEntityWithStorage<TStorageEntity>, new() where TStorageEntity : class, IStorageEntity, new() where TEntityWithStorageDownloadRequest : class, IEntityWithStorageDownloadRequest {

	protected virtual string CacheControl { get; } = Web.ResponseCache.From(CacheLocation.Private, CachePolicy.None, 24.Hours());

	protected virtual bool EnableRangeProcessing { get; }

	public override async Task<Void> HandleAsync(TEntityWithStorageDownloadRequest request, CancellationToken cancellationToken) {

		var entity = await Service.ReadAsync(request.Id, false, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		var stream = await service.DownloadAsync(entity, cancellationToken);

		return await Send.FileAsync(stream, entity.File, CacheControl, EnableRangeProcessing, cancellationToken);

	}

}
