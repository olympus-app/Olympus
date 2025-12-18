namespace Olympus.Core.Backend.Storage;

public abstract class ImageDeleteEndpoint<TEntity, TEntityWithFile> : FileDeleteEndpoint<TEntity, ImageFile, TEntityWithFile, ImageDeleteRequest> where TEntity : class, IEntity where TEntityWithFile : class, IEntityWithFile<TEntity, ImageFile> {

	protected override async Task DeleteAsync(TEntityWithFile entity, ImageDeleteRequest request, CancellationToken cancellationToken = default) {

		Database.Set<ImageFile>().Remove(entity.File);

		await Storage.DeleteAsync(entity.File.StorageBucket, entity.File.StoragePath, cancellationToken);

		foreach (var size in FastEnum.GetValues<ThumbnailSize>()) {

			var path = ImageFile.GetThumbnailPath(entity.File.StoragePath, size);

			await Storage.DeleteAsync(entity.File.StorageBucket, path, cancellationToken);

		}

		await Database.SaveChangesAsync(cancellationToken);

	}

}
