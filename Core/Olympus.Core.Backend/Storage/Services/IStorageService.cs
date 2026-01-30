namespace Olympus.Core.Backend.Storage;

public interface IStorageService {

	public Task<string> LinkAsync(string bucket, string path, int expirationSeconds, CancellationToken cancellationToken = default);

	public Task<Stream> DownloadAsync(string bucket, string path, CancellationToken cancellationToken = default);

	public Task UploadAsync(Stream stream, string bucket, string path, string contentType, CancellationToken cancellationToken = default);

	public Task DeleteAsync(string bucket, string path, CancellationToken cancellationToken = default);

}
