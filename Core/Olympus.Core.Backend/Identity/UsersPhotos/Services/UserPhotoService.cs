namespace Olympus.Core.Backend.Identity;

public class UserPhotoService(IDatabaseService database, IStorageService storage, IImageProcessor processor, IHttpContextAccessor accessor) : EntityWithImageService<UserPhoto>(database, storage, processor, accessor) {

	protected override ImageSize ImageSize => ImageSize.Medium;

	protected override ResizeMode ResizeMode => ResizeMode.Crop;

	protected override bool GenerateThumbnails => true;

	public override IQueryable<UserPhoto> Query(Guid id, bool tracking = false) {

		var query = Database.Set<UserPhoto>().AsQueryable();

		if (!tracking) query = query.AsNoTracking();

		return query.DefaultFilter().Where(uphoto => uphoto.UserId == id);

	}

}
