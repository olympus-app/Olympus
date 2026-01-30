namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithFileDeleteEndpoint<TEntityWithFile, TEntityWithFileDeleteRequest>(IEntityWithStorageService<TEntityWithFile, StorageFile> service) : EntityWithStorageDeleteEndpoint<TEntityWithFile, StorageFile, TEntityWithFileDeleteRequest>(service) where TEntityWithFile : class, IEntityWithStorage<StorageFile>, new() where TEntityWithFileDeleteRequest : class, IEntityWithStorageDeleteRequest { }
