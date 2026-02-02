namespace Olympus.Core.Backend.Storage;

public abstract class StorageEntityService<TStorageEntity>(IStorageService storage) : IStorageEntityService<TStorageEntity> where TStorageEntity : class, IStorageEntity {

	protected IStorageService Storage { get; } = storage;

	public virtual async Task<TStorageEntity> UploadAsync(Stream stream, TStorageEntity entity, CancellationToken cancellationToken = default) {

		await Storage.UploadAsync(stream, entity.Bucket, entity.Path, entity.ContentType, cancellationToken);

		entity.Size = stream.Length;

		return entity;

	}

	public virtual Task<string> LinkAsync(TStorageEntity entity, int expirationSeconds = 3600, CancellationToken cancellationToken = default) {

		return Storage.LinkAsync(entity.Bucket, entity.Path, expirationSeconds, cancellationToken);

	}

	public virtual Task<Stream> DownloadAsync(TStorageEntity entity, CancellationToken cancellationToken = default) {

		return Storage.DownloadAsync(entity.Bucket, entity.Path, cancellationToken);

	}

	public virtual Task DeleteAsync(TStorageEntity entity, CancellationToken cancellationToken = default) {

		return Storage.DeleteAsync(entity.Bucket, entity.Path, cancellationToken);

	}

}
