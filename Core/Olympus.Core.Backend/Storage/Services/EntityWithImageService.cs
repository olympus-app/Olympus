namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithImageService<TEntityWithImage>(IDatabaseService database, IStorageService storage, IImageProcessor processor, IHttpContextAccessor accessor) : EntityWithStorageService<TEntityWithImage, StorageImage>(database, storage, accessor) where TEntityWithImage : class, IEntityWithStorage<StorageImage>, new() {

	protected IImageProcessor Processor { get; } = processor;

	protected virtual ThumbnailSize? ThumbnailSize { get; }

	protected virtual ImageSize ImageSize { get; } = ImageSize.Large;

	protected virtual ResizeMode ResizeMode { get; } = ResizeMode.Max;

	protected virtual bool GenerateThumbnails { get; } = true;

	protected virtual int CompressionQuality { get; } = 80;

	public override Task<string> LinkAsync(TEntityWithImage entity, CancellationToken cancellationToken = default) {

		var path = StorageImage.GetThumbnailPath(entity.File.Path, ThumbnailSize);

		return Storage.LinkAsync(entity.File.Bucket, path, LinkExpiration, cancellationToken);

	}

	public override Task<Stream> DownloadAsync(TEntityWithImage entity, CancellationToken cancellationToken = default) {

		var path = StorageImage.GetThumbnailPath(entity.File.Path, ThumbnailSize);

		return Storage.DownloadAsync(entity.File.Bucket, path, cancellationToken);

	}

	public override async Task<TEntityWithImage> UploadAsync(TEntityWithImage entity, Stream stream, CancellationToken cancellationToken = default) {

		await using var mainStream = await Processor.ResizeAsync(stream, ImageSize.Value, ImageSize.Value, ResizeMode, CompressionQuality, cancellationToken);

		await Storage.UploadAsync(mainStream, entity.File.Bucket, entity.File.Path, entity.File.ContentType, cancellationToken);

		entity.File.Name = Path.ChangeExtension(entity.File.Name, FileExtensions.Jpeg);
		entity.File.Path = Path.ChangeExtension(entity.File.Path, FileExtensions.Jpeg);
		entity.File.ContentType = ContentTypes.ImageJpeg;
		entity.File.Size = mainStream.Length;

		if (!GenerateThumbnails) return entity;

		if (mainStream.CanSeek) mainStream.ResetPosition();

		var thumbs = await Processor.GenerateThumbnailsAsync(mainStream, ResizeMode, CompressionQuality, cancellationToken);

		try {

			foreach (var (thumbSize, thumbStream) in thumbs) {

				var thumbPath = StorageImage.GetThumbnailPath(entity.File.Path, thumbSize);

				await Storage.UploadAsync(thumbStream, entity.File.Bucket, thumbPath, entity.File.ContentType, cancellationToken);

			}

		} finally {

			foreach (var thumb in thumbs.Values) {

				await thumb.DisposeAsync();

			}

		}

		await Database.SaveChangesAsync(cancellationToken);

		return entity;

	}

	public override async Task<TEntityWithImage> DeleteAsync(TEntityWithImage entity, CancellationToken cancellationToken = default) {

		await Storage.DeleteAsync(entity.File.Bucket, entity.File.Path, cancellationToken);

		foreach (var size in FastEnum.GetValues<ThumbnailSize>()) {

			var path = StorageImage.GetThumbnailPath(entity.File.Path, size);

			await Storage.DeleteAsync(entity.File.Bucket, path, cancellationToken);

		}

		Database.Set<TEntityWithImage>().Remove(entity);

		await Database.SaveChangesAsync(cancellationToken);

		return entity;

	}

}
