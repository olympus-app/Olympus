namespace Olympus.Core.Backend.Entities;

public abstract class EntityQueryEndpoint<TEntity, TQueryRequest, TQueryResponse, TMapper> : EntityEndpoint<TEntity, TQueryRequest, QueryResult<TQueryResponse>, TMapper> where TEntity : class, IEntity where TQueryRequest : class, IEntityQueryRequest where TQueryResponse : class, IEntityQueryResponse where TMapper : class, IEntityQueryMapper<TEntity, TQueryResponse> {

	protected virtual IQueryable<TEntity> Query(TQueryRequest request) {

		return Database.Set<TEntity>().AsNoTracking().DefaultFilter().DefaultOrderBy();

	}

	protected virtual async Task<QueryResult<TQueryResponse>> QueryAsync(IQueryable<TQueryResponse> projection, TQueryRequest request, CancellationToken cancellationToken = default) {

		var config = TryResolve<IEntityQueryConfiguration<TQueryResponse>>();

		var options = new GridifyQuery(request.Page ?? 1, request.PageSize ?? 10, request.Filter, request.OrderBy);

		if (!options.IsValid(config)) ThrowError(ErrorsStrings.Values.InvalidQuery);

		var response = await projection.GridifyAsync(options, cancellationToken, config);

		var page = options.Page;
		var items = response.Data;
		var count = items.Count();
		var totalCount = response.Count;
		var totalPages = (int)Math.Ceiling((double)totalCount / (options.PageSize > 0 ? options.PageSize : 1));

		return new QueryResult<TQueryResponse>(page, count, totalPages, totalCount, items);

	}

	protected virtual void PrepareResponse(TQueryRequest request, QueryResult<TQueryResponse> response) {

		HttpContext.Response.Headers.CacheControl = Web.ResponseCache.From(CachePolicy.None);

	}

	public override async Task<Void> HandleAsync(TQueryRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var projection = Map.FromEntity(query);

		var response = await QueryAsync(projection, request, cancellationToken);

		PrepareResponse(request, response);

		return await Send.OkAsync(response, cancellationToken);

	}

}
