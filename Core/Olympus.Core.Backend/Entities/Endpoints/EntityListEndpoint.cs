using System.Collections.Concurrent;

namespace Olympus.Core.Backend.Entities;

public abstract class EntityListEndpoint<TEntity, TListRequest, TListResponse, TMapper> : EntityEndpoint<TEntity, TListRequest, ListResult<TListResponse>, TMapper> where TEntity : class, IEntity where TListRequest : class, IEntityListRequest where TListResponse : class, IEntityListResponse where TMapper : class, IEntityListMapper<TEntity, TListResponse> {

	private string ETag = string.Empty;

	private static readonly ConcurrentDictionary<Type, EntityListConfiguration> Configurations = new();

	protected EntityListConfiguration Configuration => field ?? Configurations.GetOrAdd(GetType(), static _ => new());

	protected virtual void CacheControl(CachePolicy location, TimeSpan duration, bool immutable = false) {

		Configuration.CacheControl = Web.ResponseCache.From(location, duration, immutable);

	}

	protected virtual IQueryable<TEntity> Query(TListRequest request) {

		return Database.Set<TEntity>().AsNoTracking().DefaultFilter().DefaultOrderBy().Cacheable(CacheExpirationMode.Absolute, 24.Hours());

	}

	protected virtual async Task<ListResult<TListResponse>> ListAsync(IQueryable<TListResponse> projection, TListRequest request, CancellationToken cancellationToken = default) {

		var items = await projection.ToListAsync(cancellationToken);

		return new ListResult<TListResponse>(items.Count, items);

	}

	protected virtual void PrepareResponse(TListRequest request, ListResult<TListResponse> response) {

		ETag = EntityTag.From(response);

		if (!string.IsNullOrEmpty(ETag)) HttpContext.Response.Headers.ETag = ETag;

		if (!string.IsNullOrEmpty(Configuration.CacheControl)) HttpContext.Response.Headers.CacheControl = Configuration.CacheControl;

	}

	protected virtual bool NotModifiedCheck(TListRequest request, ListResult<TListResponse> response) {

		return EntityTag.IfNoneMatch(request.ETag, ETag);

	}

	public override async Task<Void> HandleAsync(TListRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var projection = Map.FromEntity(query);

		var response = await ListAsync(projection, request, cancellationToken);

		PrepareResponse(request, response);

		var notModified = NotModifiedCheck(request, response);

		if (notModified) return await Send.NotModifiedAsync(cancellationToken);

		return await Send.OkAsync(response, cancellationToken);

	}

}
