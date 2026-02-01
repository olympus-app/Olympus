namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithStorageService<TEntityWithStorage, TStorageEntity>(IDatabaseService database, IStorageService storage, IHttpContextAccessor accessor) : EntityService<TEntityWithStorage>(database, accessor), IEntityWithStorageService<TEntityWithStorage, TStorageEntity> where TEntityWithStorage : class, IEntityWithStorage<TStorageEntity>, new() where TStorageEntity : class, IStorageEntity, new() {

	protected IStorageService Storage { get; } = storage;

	protected virtual int LinkExpiration { get; } = 3600;

	public override IQueryable<TEntityWithStorage> Query(bool tracking = false) {

		return base.Query(tracking).Include(entity => entity.File);

	}

	public override IQueryable<TEntityWithStorage> Query(Guid id, bool tracking = false) {

		return base.Query(id, tracking).Include(entity => entity.File);

	}

	public virtual Task<string> LinkAsync(TEntityWithStorage entity, CancellationToken cancellationToken = default) {

		return Storage.LinkAsync(entity.File.Bucket, entity.File.Path, LinkExpiration, cancellationToken);

	}

	public virtual Task<Stream> DownloadAsync(TEntityWithStorage entity, CancellationToken cancellationToken = default) {

		return Storage.DownloadAsync(entity.File.Bucket, entity.File.Path, cancellationToken);

	}

	public virtual async Task<TEntityWithStorage> UploadAsync(TEntityWithStorage entity, Stream stream, CancellationToken cancellationToken = default) {

		await Storage.UploadAsync(stream, entity.File.Bucket, entity.File.Path, entity.File.ContentType, cancellationToken);

		entity.File.Size = stream.Length;

		await Database.SaveChangesAsync(cancellationToken);

		return entity;

	}

	public virtual async Task<TEntityWithStorage> DeleteAsync(TEntityWithStorage entity, CancellationToken cancellationToken = default) {

		await Storage.DeleteAsync(entity.File.Bucket, entity.File.Path, cancellationToken);

		Database.Set<TEntityWithStorage>().Remove(entity);

		await Database.SaveChangesAsync(cancellationToken);

		return entity;

	}

}
