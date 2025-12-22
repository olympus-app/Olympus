using System.Collections.Concurrent;

namespace Olympus.Core.Backend.Storage;

public abstract class FileDownloadEndpoint<TEntity, TFile, TEntityWithFile, TDownloadRequest, TConfiguration> : EntityEndpoint<TEntity, TDownloadRequest> where TEntity : class, IEntity where TFile : class, IFileEntity where TEntityWithFile : class, IEntityWithFile<TEntity, TFile> where TDownloadRequest : class, IFileDownloadRequest where TConfiguration : class, IFileDownloadConfiguration, new() {

	public IStorageService Storage { get; set; } = default!;

	protected record DownloadInfo {

		public required string Name { get; set; }

		public required string ContentType { get; set; }

		public DateTimeOffset? UpdatedAt { get; set; }

		public bool RangeProcessing { get; set; }

		public long? Length { get; set; }

	}

	private static readonly ConcurrentDictionary<Type, TConfiguration> Configurations = new();

	protected TConfiguration Configuration => field ?? Configurations.GetOrAdd(GetType(), static _ => new());

	protected virtual void CacheControl(CachePolicy location, TimeSpan duration, bool immutable = false) {

		Configuration.CacheControl = Web.ResponseCache.From(location, duration, immutable);

	}

	protected virtual IQueryable<TEntityWithFile> Query(TDownloadRequest request) {

		return Database.Set<TEntityWithFile>().AsNoTracking().DefaultFilter(entity => entity.EntityId == request.Id);

	}

	protected virtual Task<TEntityWithFile?> ReadAsync(IQueryable<TEntityWithFile> query, TDownloadRequest request, CancellationToken cancellationToken = default) {

		return query.Include(entity => entity.File).SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual void PrepareResponse(TDownloadRequest request, TEntityWithFile entity) {

		if (entity.File.ETag is not null) HttpContext.Response.Headers.ETag = EntityTag.From(entity.File.ETag);

		if (!string.IsNullOrEmpty(Configuration.CacheControl)) HttpContext.Response.Headers.CacheControl = Configuration.CacheControl;

	}

	protected virtual bool NotModifiedCheck(TDownloadRequest request, TEntityWithFile entity) {

		return EntityTag.IfNoneMatch(request.ETag, entity.File.ETag);

	}

	protected virtual DownloadInfo PrepareDownload(TFile file, TDownloadRequest request) {

		return new DownloadInfo() {
			Name = file.Name,
			ContentType = file.ContentType,
			UpdatedAt = file.UpdatedAt,
			Length = file.Size,
		};

	}

	protected virtual Task<Stream> DownloadAsync(TFile file, TDownloadRequest request, CancellationToken cancellationToken = default) {

		return Storage.DownloadAsync(file.StorageBucket, file.StoragePath, cancellationToken);

	}

	public override async Task<Void> HandleAsync(TDownloadRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var entity = await ReadAsync(query, request, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		PrepareResponse(request, entity);

		var notModified = NotModifiedCheck(request, entity);

		if (notModified) return await Send.NotModifiedAsync(cancellationToken);

		var info = PrepareDownload(entity.File, request);

		var stream = await DownloadAsync(entity.File, request, cancellationToken);

		return await Send.StreamAsync(stream, info.Name, info.Length, info.ContentType, info.UpdatedAt, info.RangeProcessing, cancellationToken);

	}

}
