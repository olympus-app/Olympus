namespace Olympus.Core.Archend.Identity;

public record UserPhotoLinkResponse : EntityWithFileLinkResponse<ImageLinkResponse> {

	public string GetPhotoUrl() {

		return Routes.FromApi(1).Append(CoreRoutes.Users.Photo).WithId(EntityId).WithCacheBusting(File.UpdatedAt);

	}

}
