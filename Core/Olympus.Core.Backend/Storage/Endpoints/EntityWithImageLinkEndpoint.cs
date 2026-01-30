namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithImageLinkEndpoint<TEntityWithImage, TEntityWithImageLinkRequest>(IEntityWithStorageService<TEntityWithImage, StorageImage> service) : EntityWithStorageLinkEndpoint<TEntityWithImage, StorageImage, TEntityWithImageLinkRequest>(service) where TEntityWithImage : class, IEntityWithStorage<StorageImage>, new() where TEntityWithImageLinkRequest : class, IEntityWithStorageLinkRequest { }
