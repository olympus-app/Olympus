namespace Olympus.Core.Backend.Storage;

public interface IStorageFileService : IStorageEntityService<StorageFile>, ISingletonService<IStorageFileService> {

	public new Task<StorageFile> UploadAsync(Stream stream, StorageFile file, CancellationToken cancellationToken = default);

	public new Task<string> LinkAsync(StorageFile file, int expirationSeconds, CancellationToken cancellationToken = default);

	public new Task<Stream> DownloadAsync(StorageFile file, CancellationToken cancellationToken = default);

	public new Task DeleteAsync(StorageFile file, CancellationToken cancellationToken = default);

}
