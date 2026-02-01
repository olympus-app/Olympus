namespace Olympus.Core.Backend.Storage;

public interface IEntityWithStorageService<TEntityWithStorage, TStorageEntity> : IScopedService<IEntityWithStorageService<TEntityWithStorage, TStorageEntity>>, IEntityService<TEntityWithStorage> where TEntityWithStorage : class, IEntityWithStorage<TStorageEntity>, new() where TStorageEntity : class, IStorageEntity, new() {

	public Task<string> LinkAsync(TEntityWithStorage entity, CancellationToken cancellationToken = default);

	public Task<Stream> DownloadAsync(TEntityWithStorage entity, CancellationToken cancellationToken = default);

	public Task<TEntityWithStorage> UploadAsync(TEntityWithStorage entity, Stream stream, CancellationToken cancellationToken = default);

	public Task<TEntityWithStorage> DeleteAsync(TEntityWithStorage entity, CancellationToken cancellationToken = default);

}
