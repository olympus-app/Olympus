namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithFileService<TEntityWithFile>(IDatabaseService database, IStorageService storage, IHttpContextAccessor accessor) : EntityWithStorageService<TEntityWithFile, StorageFile>(database, storage, accessor) where TEntityWithFile : class, IEntityWithStorage<StorageFile>, new() { }
