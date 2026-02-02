namespace Olympus.Core.Backend.Identity;

public class UserPhotoUploadEndpoint(UserService service) : EntityEndpoint<User>.WithRequest<UserPhotoUploadRequest>(service) {

	public override void Configure() {

		Put(CoreRoutes.Users.Photo);

	}

	public override async Task<Void> HandleAsync(UserPhotoUploadRequest request, CancellationToken cancellationToken) {

		if (Files.Count == 0) return await Send.BadRequestAsync(ErrorsStrings.Values.NoFileSent, cancellationToken);

		var user = await service.Query(request.Id, true).Include(user => user.Photo).SingleOrDefaultAsync(cancellationToken);

		if (user is null) return await Send.NotFoundAsync(cancellationToken);

		user.Photo ??= UserService.InitializePhoto(request.Id);

		if (ConflictCheck(user.Photo.ETag)) return await Send.ConflictAsync(cancellationToken);

		user.Photo = await service.UploadPhotoAsync(Files[0].OpenReadStream(), user.Photo, cancellationToken);

		await service.SaveChangesAsync(cancellationToken);

		return await Send.UploadedAsync(user.Photo, cancellationToken);

	}

}
