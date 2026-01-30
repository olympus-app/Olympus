namespace Olympus.Core.Backend.Identity;

public class UserPhotoDeleteEndpoint(UserPhotoService service) : EntityWithImageDeleteEndpoint<UserPhoto, UserPhotoDeleteRequest>(service) {

	public override void Configure() {

		Delete(CoreRoutes.Users.Photo);

	}

}
