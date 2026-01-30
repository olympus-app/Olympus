namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithImageDeleteEndpoint<TEntityWithImage, TEntityWithImageDeleteRequest>(IEntityWithStorageService<TEntityWithImage, StorageImage> service) : EntityWithStorageDeleteEndpoint<TEntityWithImage, StorageImage, TEntityWithImageDeleteRequest>(service) where TEntityWithImage : class, IEntityWithStorage<StorageImage>, new() where TEntityWithImageDeleteRequest : class, IEntityWithStorageDeleteRequest { }
