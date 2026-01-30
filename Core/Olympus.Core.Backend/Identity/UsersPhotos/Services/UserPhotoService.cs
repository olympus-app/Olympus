namespace Olympus.Core.Backend.Identity;

public class UserPhotoService(IDatabaseService database, IStorageService storage, IImageProcessor processor, IHttpContextAccessor accessor) : EntityWithImageService<UserPhoto>(database, storage, processor, accessor) {

	protected override ImageSize ImageSize => ImageSize.Medium;

	protected override ResizeMode ResizeMode => ResizeMode.Crop;

	protected override bool GenerateThumbnails => true;

}
