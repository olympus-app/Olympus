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

	protected virtual void CacheControl(int durationSeconds, ResponseCacheLocation location = ResponseCacheLocation.None, bool immutable = false) {

		var locationValue = location switch {
			ResponseCacheLocation.Any => "public",
			ResponseCacheLocation.Client => "private",
			_ => "no-cache",
		};

		var immutableValue = immutable && location != ResponseCacheLocation.None ? ", immutable" : string.Empty;

		Configuration.CacheControl = $"{locationValue}, max-age={durationSeconds}{immutableValue}";

	}

	protected virtual IQueryable<TEntityWithFile> Query(TDownloadRequest request) {

		return Database.Set<TEntityWithFile>().AsNoTracking().DefaultFilter(entity => entity.EntityId == request.Id);

	}

	protected virtual Task<TEntityWithFile?> ReadAsync(IQueryable<TEntityWithFile> query, TDownloadRequest request, CancellationToken cancellationToken = default) {

		return query.Include(entity => entity.File).SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual bool NotModifiedCheck(TEntityWithFile entity, TDownloadRequest request) {

		HttpContext.Response.Headers.ETag = $"\"{entity.File.ContentHash}\"";

		return EntityTag.Match(HttpContext.Request.Headers.ETag, HttpContext.Response.Headers.ETag);

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

		var notModified = NotModifiedCheck(entity, request);

		if (notModified) return await Send.NotModifiedAsync(cancellationToken);

		if (!string.IsNullOrEmpty(Configuration.CacheControl)) HttpContext.Response.Headers.CacheControl = Configuration.CacheControl;

		var info = PrepareDownload(entity.File, request);

		var stream = await DownloadAsync(entity.File, request, cancellationToken);

		return await Send.StreamAsync(stream, info.Name, info.Length, info.ContentType, info.UpdatedAt, info.RangeProcessing, cancellationToken);

	}

}
