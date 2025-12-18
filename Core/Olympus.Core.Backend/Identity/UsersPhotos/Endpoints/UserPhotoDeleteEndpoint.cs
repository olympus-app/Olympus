namespace Olympus.Core.Backend.Identity;

public class UserPhotoDeleteEndpoint : ImageDeleteEndpoint<User, UserPhoto> {

	public override void Configure() {

		Delete(CoreRoutes.Users.Photo);

	}

}
