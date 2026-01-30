namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithFileDownloadEndpoint<TEntityWithFile, TEntityWithFileDownloadRequest>(IEntityWithStorageService<TEntityWithFile, StorageFile> service) : EntityWithStorageDownloadEndpoint<TEntityWithFile, StorageFile, TEntityWithFileDownloadRequest>(service) where TEntityWithFile : class, IEntityWithStorage<StorageFile>, new() where TEntityWithFileDownloadRequest : class, IEntityWithStorageDownloadRequest { }
