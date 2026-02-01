namespace Olympus.Core.Backend.Identity;

public class UserPhotoUploadEndpoint(UserPhotoService service) : EntityWithImageUploadEndpoint<UserPhoto, UserPhotoUploadRequest>(service) {

	public override void Configure() {

		Post(CoreRoutes.Users.Photo);

	}

	protected override UserPhoto Initialize(UserPhotoUploadRequest request, IFormFile file) {

		return new UserPhoto() {
			Id = request.Id,
			UserId = request.Id,
			File = new StorageImage() {
				Name = "profile.jpg",
				ContentType = ContentTypes.ImageJpeg,
				Bucket = StorageLocation.Public,
				Path = "Users/profile.jpg",
				Size = file.Length,
			},
		};

	}

}
