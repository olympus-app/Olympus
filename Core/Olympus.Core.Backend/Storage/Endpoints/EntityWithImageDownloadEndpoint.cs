namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithImageDownloadEndpoint<TEntityWithImage, TEntityWithImageDownloadRequest>(IEntityWithStorageService<TEntityWithImage, StorageImage> service) : EntityWithStorageDownloadEndpoint<TEntityWithImage, StorageImage, TEntityWithImageDownloadRequest>(service) where TEntityWithImage : class, IEntityWithStorage<StorageImage>, new() where TEntityWithImageDownloadRequest : class, IEntityWithStorageDownloadRequest { }
