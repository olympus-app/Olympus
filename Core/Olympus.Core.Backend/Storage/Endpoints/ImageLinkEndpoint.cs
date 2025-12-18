namespace Olympus.Core.Backend.Storage;

public abstract class ImageLinkEndpoint<TEntity, TEntityWithFile> : FileLinkEndpoint<TEntity, ImageFile, TEntityWithFile, ImageLinkRequest, ImageLinkConfiguration> where TEntity : class, IEntity where TEntityWithFile : class, IEntityWithFile<TEntity, ImageFile> {

	protected override Task<string> LinkAsync(TEntityWithFile entity, ImageLinkRequest request, CancellationToken cancellationToken = default) {

		var path = ImageFile.GetThumbnailPath(entity.File.StoragePath, request.Size);

		return Storage.LinkAsync(entity.File.StorageBucket, path, Configuration.Expiration, cancellationToken);

	}

}
