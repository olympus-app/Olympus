namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithImageService<TEntityWithImage>(IDatabaseService database, IStorageService storage, IImageProcessor processor, IHttpContextAccessor accessor) : EntityWithStorageService<TEntityWithImage, StorageImage>(database, storage, accessor) where TEntityWithImage : class, IEntityWithStorage<StorageImage>, new() {

	protected IImageProcessor Processor { get; } = processor;

	protected virtual ThumbnailSize? ThumbnailSize { get; }

	protected virtual ImageSize ImageSize { get; } = ImageSize.Large;

	protected virtual ResizeMode ResizeMode { get; } = ResizeMode.Max;

	protected virtual bool GenerateThumbnails { get; } = true;

	protected virtual int CompressionQuality { get; } = 80;

	public override Task<string> LinkAsync(TEntityWithImage entity, CancellationToken cancellationToken = default) {

		var path = StorageImage.GetThumbnailPath(entity.File.StoragePath, ThumbnailSize);

		return Storage.LinkAsync(entity.File.StorageBucket.Name, path, LinkExpiration, cancellationToken);

	}

	public override Task<Stream> DownloadAsync(TEntityWithImage entity, CancellationToken cancellationToken = default) {

		var path = StorageImage.GetThumbnailPath(entity.File.StoragePath, ThumbnailSize);

		return Storage.DownloadAsync(entity.File.StorageBucket.Name, path, cancellationToken);

	}

	public override async Task<TEntityWithImage> UploadAsync(TEntityWithImage entity, Stream stream, CancellationToken cancellationToken = default) {

		await using var mainStream = await Processor.ResizeAsync(stream, ImageSize.Value, ImageSize.Value, ResizeMode, CompressionQuality, cancellationToken);

		await Storage.UploadAsync(mainStream, entity.File.StorageBucket.Name, entity.File.StoragePath, entity.File.ContentType, cancellationToken);

		entity.File.Name = Path.ChangeExtension(entity.File.Name, FileExtensions.Jpeg);
		entity.File.StoragePath = Path.ChangeExtension(entity.File.StoragePath, FileExtensions.Jpeg);
		entity.File.ContentType = ContentTypes.ImageJpeg;
		entity.File.Size = mainStream.Length;

		if (!GenerateThumbnails) return entity;

		if (mainStream.CanSeek) mainStream.ResetPosition();

		var thumbs = await Processor.GenerateThumbnailsAsync(mainStream, ResizeMode, CompressionQuality, cancellationToken);

		try {

			foreach (var (thumbSize, thumbStream) in thumbs) {

				var thumbPath = StorageImage.GetThumbnailPath(entity.File.StoragePath, thumbSize);

				await Storage.UploadAsync(thumbStream, entity.File.StorageBucket.Name, thumbPath, entity.File.ContentType, cancellationToken);

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

		await Storage.DeleteAsync(entity.File.StorageBucket.Name, entity.File.StoragePath, cancellationToken);

		foreach (var size in FastEnum.GetValues<ThumbnailSize>()) {

			var path = StorageImage.GetThumbnailPath(entity.File.StoragePath, size);

			await Storage.DeleteAsync(entity.File.StorageBucket.Name, path, cancellationToken);

		}

		Database.Set<TEntityWithImage>().Remove(entity);

		await Database.SaveChangesAsync(cancellationToken);

		return entity;

	}

}
