namespace Olympus.Core.Backend.Identity;

public class UserPhotoUploadEndpoint : ImageUploadEndpoint<User, UserPhoto> {

	public override void Configure() {

		Post(CoreRoutes.Users.Photo);

		StorageOptions(StorageLocation.Public, fileName: "profile.jpg");
		ProcessorOptions(ImageSize.Medium, ResizeMode.Crop, true);

	}

}
