namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithFileLinkEndpoint<TEntityWithFile, TEntityWithFileLinkRequest>(IEntityWithStorageService<TEntityWithFile, StorageFile> service) : EntityWithStorageLinkEndpoint<TEntityWithFile, StorageFile, TEntityWithFileLinkRequest>(service) where TEntityWithFile : class, IEntityWithStorage<StorageFile>, new() where TEntityWithFileLinkRequest : class, IEntityWithStorageLinkRequest { }
