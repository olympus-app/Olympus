namespace Olympus.Core.Backend.Identity;

public class UserPhoto : EntityWithImage {

	public Guid UserId { get; set; }

	public User User { get; set; } = null!;

	public string GetPhotoUrl() {

		return AppUriBuilder.FromApi(CoreRoutes.Users.Photo).WithId(UserId).WithCacheBusting(File.UpdatedAt);

	}

}
