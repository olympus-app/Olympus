namespace Olympus.Core.Backend.Storage;

public interface IStorageService {

	public Task UploadAsync(Stream stream, StorageLocation bucket, string path, string contentType, CancellationToken cancellationToken = default);

	public Task<string> LinkAsync(StorageLocation bucket, string path, int expirationSeconds, CancellationToken cancellationToken = default);

	public Task<Stream> DownloadAsync(StorageLocation bucket, string path, CancellationToken cancellationToken = default);

	public Task DeleteAsync(StorageLocation bucket, string path, CancellationToken cancellationToken = default);

}
