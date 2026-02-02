namespace Olympus.Core.Backend.Identity;

public class UserService(IDatabaseService database, IStorageImageService storage, IHttpContextAccessor accessor) : EntityService<User>(database, accessor) {

	private IStorageImageService Storage { get; } = storage;

	public static StorageImage InitializePhoto(Guid id) {

		return new StorageImage() {
			Name = "profile.jpg",
			ContentType = ContentTypes.ImageJpeg,
			Bucket = StorageLocation.Public,
			Path = $"Users/{id.NormalizeLower()}/profile.jpg",
		};

	}

	public Task<StorageImage> UploadPhotoAsync(Stream stream, StorageImage photo, CancellationToken cancellationToken = default) {

		Database.Set<StorageImage>().Update(photo, true);

		return Storage.UploadAsync(stream, photo, ImageSize.Medium, ResizeMode.Crop, 80, true, cancellationToken);

	}

	public Task<Stream> DownloadPhotoAsync(StorageImage photo, ThumbnailSize? size = null, CancellationToken cancellationToken = default) {

		return Storage.DownloadAsync(photo, size, cancellationToken);

	}

	public Task DeletePhotoAsync(StorageImage photo, CancellationToken cancellationToken = default) {

		Database.Set<StorageImage>().Remove(photo);

		return Storage.DeleteAsync(photo, cancellationToken);

	}

}
