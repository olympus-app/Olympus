namespace Olympus.Core.Backend.Storage;

public class StorageImageService(IStorageService storage, IImageProcessor processor) : StorageEntityService<StorageImage>(storage), IStorageImageService {

	protected IImageProcessor Processor { get; } = processor;

	public async Task<StorageImage> UploadAsync(Stream stream, StorageImage image, ImageSize size = ImageSize.Large, ResizeMode mode = ResizeMode.Max, int quality = 80, bool thumbnails = true, CancellationToken cancellationToken = default) {

		await using var mainStream = await Processor.ResizeAsync(stream, size.Value, size.Value, mode, quality, cancellationToken);

		await Storage.UploadAsync(mainStream, image.Bucket, image.Path, image.ContentType, cancellationToken);

		image.Name = Path.ChangeExtension(image.Name, FileExtensions.Jpeg);
		image.Path = Path.ChangeExtension(image.Path, FileExtensions.Jpeg);
		image.ContentType = ContentTypes.ImageJpeg;
		image.Size = mainStream.Length;

		if (!thumbnails) return image;

		if (mainStream.CanSeek) mainStream.ResetPosition();

		var thumbs = await Processor.GenerateThumbnailsAsync(mainStream, mode, quality, cancellationToken);

		try {

			foreach (var (thumbSize, thumbStream) in thumbs) {

				var thumbPath = StorageImage.GetThumbnailPath(image.Path, thumbSize);

				await Storage.UploadAsync(thumbStream, image.Bucket, thumbPath, image.ContentType, cancellationToken);

			}

		} finally {

			foreach (var thumb in thumbs.Values) {

				await thumb.DisposeAsync();

			}

		}

		return image;

	}

	public Task<string> LinkAsync(StorageImage image, ThumbnailSize? size = null, int expirationSeconds = 3600, CancellationToken cancellationToken = default) {

		var path = StorageImage.GetThumbnailPath(image.Path, size);

		return Storage.LinkAsync(image.Bucket, path, expirationSeconds, cancellationToken);

	}

	public Task<Stream> DownloadAsync(StorageImage image, ThumbnailSize? size = null, CancellationToken cancellationToken = default) {

		var path = StorageImage.GetThumbnailPath(image.Path, size);

		return Storage.DownloadAsync(image.Bucket, path, cancellationToken);

	}

	public override async Task DeleteAsync(StorageImage image, CancellationToken cancellationToken = default) {

		await Storage.DeleteAsync(image.Bucket, image.Path, cancellationToken);

		foreach (var size in FastEnum.GetValues<ThumbnailSize>()) {

			var path = StorageImage.GetThumbnailPath(image.Path, size);

			await Storage.DeleteAsync(image.Bucket, path, cancellationToken);

		}

	}

}
