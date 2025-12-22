namespace Olympus.Core.Backend.Storage;

public abstract class ImageUploadEndpoint<TEntity, TEntityWithFile> : FileUploadEndpoint<TEntity, ImageFile, TEntityWithFile, ImageUploadRequest, ImageUploadConfiguration> where TEntity : class, IEntity where TEntityWithFile : class, IEntityWithFile<TEntity, ImageFile>, new() {

	public IImageProcessor Processor { get; set; } = default!;

	protected void ProcessorOptions(ImageSize imageSize = ImageSize.Large, ResizeMode resizeMode = ResizeMode.Max, bool thumbnails = true, int quality = 80) {

		Configuration.ImageSize = imageSize;
		Configuration.ResizeMode = resizeMode;
		Configuration.GenerateThumbnails = thumbnails;
		Configuration.CompressionQuality = quality;

	}

	protected override void StorageOptions(StorageLocation bucket = StorageLocation.Public, string? folderName = null, string? fileName = null, string? contentType = null) {

		base.StorageOptions(bucket, folderName, fileName, contentType);

	}

	protected override async Task UploadAsync(TEntityWithFile? entity, IFormFile upload, ImageUploadRequest request, CancellationToken cancellationToken = default) {

		if (entity is null) {

			entity = Initialize(upload, request);

			Database.Set<TEntityWithFile>().Add(entity);

		} else {

			Database.Set<TEntityWithFile>().Update(entity);

			Database.Entry(entity.File).State = EntityState.Modified;

			await Storage.DeleteAsync(entity.File.StorageBucket, entity.File.StoragePath, cancellationToken);

			foreach (var size in FastEnum.GetValues<ThumbnailSize>()) {

				var path = ImageFile.GetThumbnailPath(entity.File.StoragePath, size);

				await Storage.DeleteAsync(entity.File.StorageBucket, path, cancellationToken);

			}

		}

		var stream = upload.OpenReadStream();

		await using var mainStream = await Processor.ResizeAsync(stream, Configuration.ImageSize.Value, Configuration.ImageSize.Value, Configuration.ResizeMode, Configuration.CompressionQuality, cancellationToken);

		await Storage.UploadAsync(mainStream, entity.File.StorageBucket, entity.File.StoragePath, entity.File.ContentType, cancellationToken);

		entity.File.Size = mainStream.Length;
		entity.File.Name = entity.File.BaseName + FileExtensions.Jpeg;
		entity.File.Extension = FileExtensions.Jpeg;
		entity.File.ContentType = ContentTypes.ImageJpeg;
		entity.File.StoragePath = Path.ChangeExtension(entity.File.StoragePath, FileExtensions.Jpeg);

		if (!Configuration.GenerateThumbnails) return;

		if (mainStream.CanSeek) mainStream.ResetPosition();

		var thumbnails = await Processor.GenerateThumbnailsAsync(mainStream, Configuration.ResizeMode, Configuration.CompressionQuality, cancellationToken);

		if (thumbnails is null) return;

		try {

			foreach (var (thumbSize, thumbStream) in thumbnails) {

				var thumbPath = ImageFile.GetThumbnailPath(entity.File.StoragePath, thumbSize);

				await Storage.UploadAsync(thumbStream, entity.File.StorageBucket, thumbPath, entity.File.ContentType, cancellationToken);

			}

		} finally {

			foreach (var thumbnail in thumbnails.Values) {

				await thumbnail.DisposeAsync();

			}

		}

		await Database.SaveChangesAsync(cancellationToken);

	}

}
