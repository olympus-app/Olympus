namespace Olympus.Core.Backend.Storage;

public abstract class FileDeleteEndpoint<TEntity, TFile, TEntityWithFile, TDeleteRequest> : EntityEndpoint<TEntity, TDeleteRequest> where TEntity : class, IEntity where TFile : class, IFileEntity where TEntityWithFile : class, IEntityWithFile<TEntity, TFile> where TDeleteRequest : class, IFileDeleteRequest {

	public IStorageService Storage { get; set; } = default!;

	protected virtual IQueryable<TEntityWithFile> Query(TDeleteRequest request) {

		return Database.Set<TEntityWithFile>().AsTracking().DefaultFilter(entity => entity.EntityId == request.Id, deleted: null);

	}

	protected virtual Task<TEntityWithFile?> ReadAsync(IQueryable<TEntityWithFile> query, TDeleteRequest request, CancellationToken cancellationToken = default) {

		return query.Include(entity => entity.File).SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual async Task DeleteAsync(TEntityWithFile entity, TDeleteRequest request, CancellationToken cancellationToken = default) {

		Database.Set<TFile>().Remove(entity.File);

		await Storage.DeleteAsync(entity.File.StorageBucket, entity.File.StoragePath, cancellationToken);

		await Database.SaveChangesAsync(cancellationToken);

	}

	public override async Task<Void> HandleAsync(TDeleteRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var entity = await ReadAsync(query, request, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		await DeleteAsync(entity, request, cancellationToken);

		return await Send.DeletedAsync(cancellationToken);

	}

}
