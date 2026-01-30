namespace Olympus.Core.Backend.Identity;

public class UserPhotoUploadEndpoint(UserPhotoService service) : EntityWithImageUploadEndpoint<UserPhoto, UserPhotoUploadRequest>(service) {

	public override void Configure() {

		Post(CoreRoutes.Users.Photo);

	}

}
