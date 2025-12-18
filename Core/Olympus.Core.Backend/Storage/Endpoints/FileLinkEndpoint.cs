using System.Collections.Concurrent;

namespace Olympus.Core.Backend.Storage;

public abstract class FileLinkEndpoint<TEntity, TFile, TEntityWithFile, TLinkRequest, TConfiguration> : EntityEndpoint<TEntity, TLinkRequest> where TEntity : class, IEntity where TFile : class, IFileEntity where TEntityWithFile : class, IEntityWithFile<TEntity, TFile> where TLinkRequest : class, IFileLinkRequest where TConfiguration : class, IFileLinkConfiguration, new() {

	public IStorageService Storage { get; set; } = default!;

	private static readonly ConcurrentDictionary<Type, TConfiguration> Configurations = new();

	protected TConfiguration Configuration => field ?? Configurations.GetOrAdd(GetType(), static _ => new());

	protected virtual void LinkExpiration(int expirationSeconds = 3600) {

		Configuration.Expiration = expirationSeconds;

	}

	protected virtual IQueryable<TEntityWithFile> Query(TLinkRequest request) {

		return Database.Set<TEntityWithFile>().AsNoTracking().DefaultFilter(entity => entity.EntityId == request.Id);

	}

	protected virtual Task<TEntityWithFile?> ReadAsync(IQueryable<TEntityWithFile> query, TLinkRequest request, CancellationToken cancellationToken = default) {

		return query.Include(entity => entity.File).SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual Task<string> LinkAsync(TEntityWithFile entity, TLinkRequest request, CancellationToken cancellationToken = default) {

		return Storage.LinkAsync(entity.File.StorageBucket, entity.File.StoragePath, Configuration.Expiration, cancellationToken);

	}

	public override async Task<Void> HandleAsync(TLinkRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var entity = await ReadAsync(query, request, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		var link = await LinkAsync(entity, request, cancellationToken);

		return await Send.StringAsync(link, cancellation: cancellationToken);

	}

}
