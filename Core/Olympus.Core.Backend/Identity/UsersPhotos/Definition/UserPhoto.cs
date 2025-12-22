namespace Olympus.Core.Backend.Identity;

public class UserPhoto : EntityWithFile<User, ImageFile> {

	public string GetPhotoUrl() {

		return Routes.FromApi(1).Append(CoreRoutes.Users.Photo).WithId(EntityId).WithCacheBusting(File.UpdatedAt);

	}

}
