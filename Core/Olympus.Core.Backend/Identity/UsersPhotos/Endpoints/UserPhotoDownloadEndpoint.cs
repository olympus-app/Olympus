namespace Olympus.Core.Backend.Identity;

public class UserPhotoDownloadEndpoint : ImageDownloadEndpoint<User, UserPhoto> {

	public override void Configure() {

		Get(CoreRoutes.Users.Photo);

		CacheControl(31536000, ResponseCacheLocation.Any, true);

	}

}
