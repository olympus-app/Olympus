namespace Olympus.Core.Archend.Identity;

public record UserPhotoLinkResponse : EntityWithImageLinkResponse {

	public Guid UserId { get; set; }

	public string GetPhotoUrl() {

		return AppUriBuilder.FromApi(CoreRoutes.Users.Photo).WithId(UserId).WithCacheBusting(File.UpdatedAt);

	}

}
