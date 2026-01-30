namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithFileUploadEndpoint<TEntityWithFile, TEntityWithFileUploadRequest>(IEntityWithStorageService<TEntityWithFile, StorageFile> service) : EntityWithStorageUploadEndpoint<TEntityWithFile, StorageFile, TEntityWithFileUploadRequest>(service) where TEntityWithFile : class, IEntityWithStorage<StorageFile>, new() where TEntityWithFileUploadRequest : class, IEntityWithStorageUploadRequest { }
