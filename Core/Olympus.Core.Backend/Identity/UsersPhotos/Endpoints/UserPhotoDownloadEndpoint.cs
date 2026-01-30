namespace Olympus.Core.Backend.Identity;

public class UserPhotoDownloadEndpoint(UserPhotoService service) : EntityWithImageDownloadEndpoint<UserPhoto, UserPhotoDownloadRequest>(service) {

	protected override string CacheControl => Web.ResponseCache.From(CacheLocation.Private, CachePolicy.Immutable, 365.Days());

	public override void Configure() {

		Get(CoreRoutes.Users.Photo);

	}

}
