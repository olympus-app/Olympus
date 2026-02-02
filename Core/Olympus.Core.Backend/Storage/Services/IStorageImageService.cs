namespace Olympus.Core.Backend.Storage;

public interface IStorageImageService : IStorageEntityService<StorageImage>, ISingletonService<IStorageImageService> {

	public Task<StorageImage> UploadAsync(Stream stream, StorageImage image, ImageSize size, ResizeMode mode, int quality, bool thumbnails, CancellationToken cancellationToken = default);

	public Task<string> LinkAsync(StorageImage image, ThumbnailSize? size, int expirationSeconds, CancellationToken cancellationToken = default);

	public Task<Stream> DownloadAsync(StorageImage image, ThumbnailSize? size, CancellationToken cancellationToken = default);

	public new Task DeleteAsync(StorageImage image, CancellationToken cancellationToken = default);

}
