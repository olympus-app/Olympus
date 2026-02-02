namespace Olympus.Core.Backend.Identity;

public class UserPhotoDownloadEndpoint(UserService service) : EntityEndpoint<User>.WithRequest<UserPhotoDownloadRequest>(service) {

	public override void Configure() {

		Get(CoreRoutes.Users.Photo);

	}

	public override async Task<Void> HandleAsync(UserPhotoDownloadRequest request, CancellationToken cancellationToken) {

		var photo = await service.Query(request.Id, false).Select(user => user.Photo).SingleOrDefaultAsync(cancellationToken);

		if (photo is null) return await Send.NotFoundAsync(cancellationToken);

		if (NotModifiedCheck(photo.ETag)) return await Send.NotModifiedAsync(cancellationToken);

		var stream = await service.DownloadPhotoAsync(photo, request.Size, cancellationToken);

		var cache = Web.ResponseCache.From(CacheLocation.Private, CachePolicy.Immutable, 365.Days());

		return await Send.ImageAsync(stream, photo, request.Size, cache, false, cancellationToken);

	}

}
