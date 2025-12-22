namespace Olympus.Core.Backend.Identity;

public class UserPhotoDownloadEndpoint : ImageDownloadEndpoint<User, UserPhoto> {

	public override void Configure() {

		Get(CoreRoutes.Users.Photo);

		CacheControl(CachePolicy.Private, 365.Days(), true);

	}

}
