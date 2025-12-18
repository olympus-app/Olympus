namespace Olympus.Core.Backend.Storage;

public abstract class ImageDownloadEndpoint<TEntity, TEntityWithFile> : FileDownloadEndpoint<TEntity, ImageFile, TEntityWithFile, ImageDownloadRequest, ImageDownloadConfiguration> where TEntity : class, IEntity where TEntityWithFile : class, IEntityWithFile<TEntity, ImageFile> {

	protected override void CacheControl(int durationSeconds = 3600, ResponseCacheLocation location = ResponseCacheLocation.Client, bool immutable = false) {

		base.CacheControl(durationSeconds, location, immutable);

	}

	protected override DownloadInfo PrepareDownload(ImageFile image, ImageDownloadRequest request) {

		return new DownloadInfo() {
			Name = image.Name,
			ContentType = image.ContentType,
			UpdatedAt = image.UpdatedAt,
			Length = request.Size is null ? image.Size : null,
		};

	}

	protected override Task<Stream> DownloadAsync(ImageFile image, ImageDownloadRequest request, CancellationToken cancellationToken = default) {

		var path = ImageFile.GetThumbnailPath(image.StoragePath, request.Size);

		return Storage.DownloadAsync(image.StorageBucket, path, cancellationToken);

	}

}
