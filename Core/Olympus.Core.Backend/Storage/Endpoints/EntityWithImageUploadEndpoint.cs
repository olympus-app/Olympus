namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithImageUploadEndpoint<TEntityWithImage, TEntityWithImageUploadRequest>(IEntityWithStorageService<TEntityWithImage, StorageImage> service) : EntityWithStorageUploadEndpoint<TEntityWithImage, StorageImage, TEntityWithImageUploadRequest>(service) where TEntityWithImage : class, IEntityWithStorage<StorageImage>, new() where TEntityWithImageUploadRequest : class, IEntityWithStorageUploadRequest { }
