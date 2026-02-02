namespace Olympus.Core.Backend.Identity;

public class UserPhotoDeleteEndpoint(UserService service) : EntityEndpoint<User>.WithRequest<UserPhotoDeleteRequest>(service) {

	public override void Configure() {

		Delete(CoreRoutes.Users.Photo);

	}

	public override async Task<Void> HandleAsync(UserPhotoDeleteRequest request, CancellationToken cancellationToken) {

		var photo = await service.Query(request.Id, true).Select(user => user.Photo!).SingleOrDefaultAsync(cancellationToken);

		if (photo is null) return await Send.NotFoundAsync(cancellationToken);

		if (ConflictCheck(photo.ETag)) return await Send.ConflictAsync(cancellationToken);

		await service.DeletePhotoAsync(photo, cancellationToken);

		await service.SaveChangesAsync(cancellationToken);

		return await Send.DeletedAsync(photo, cancellationToken);

	}

}
