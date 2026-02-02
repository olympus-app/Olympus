namespace Olympus.Core.Backend.Storage;

public interface IStorageEntityService<TStorageEntity> : ISingletonService<IStorageEntityService<TStorageEntity>> where TStorageEntity : class, IStorageEntity {

	public Task<TStorageEntity> UploadAsync(Stream stream, TStorageEntity entity, CancellationToken cancellationToken = default);

	public Task<string> LinkAsync(TStorageEntity entity, int expirationSeconds, CancellationToken cancellationToken = default);

	public Task<Stream> DownloadAsync(TStorageEntity entity, CancellationToken cancellationToken = default);

	public Task DeleteAsync(TStorageEntity entity, CancellationToken cancellationToken = default);

}
