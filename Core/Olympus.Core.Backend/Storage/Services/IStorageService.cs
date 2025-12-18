namespace Olympus.Core.Backend.Storage;

public interface IStorageService {

	public Task<string> LinkAsync(string storageBucket, string storagePath, int expirationSeconds, CancellationToken cancellationToken = default);

	public Task<Stream> DownloadAsync(string storageBucket, string storagePath, CancellationToken cancellationToken = default);

	public Task UploadAsync(Stream stream, string storageBucket, string storagePath, string contentType, CancellationToken cancellationToken = default);

	public Task DeleteAsync(string storageBucket, string storagePath, CancellationToken cancellationToken = default);

}
