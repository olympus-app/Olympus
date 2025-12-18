namespace Olympus.Core.Backend.Entities;

public abstract class EntityListEndpoint<TEntity, TListRequest, TListResponse, TMapper> : EntityEndpoint<TEntity, TListRequest, PageResult<TListResponse>, TMapper> where TEntity : class, IEntity where TListRequest : class, IEntityListRequest where TListResponse : class, IEntityListResponse where TMapper : class, IEntityListMapper<TEntity, TListResponse> {

	protected virtual IQueryable<TEntity> Query(TListRequest request) {

		return Database.Set<TEntity>().AsNoTracking().DefaultFilter().DefaultOrderBy();

	}

	protected virtual async Task<PageResult<TListResponse>> ListAsync(IQueryable<TListResponse> projection, TListRequest request, CancellationToken cancellationToken = default) {

		var config = TryResolve<IEntityQueryMapper<TListResponse>>();

		var options = new GridifyQuery(request.Page, request.PageSize, request.Filter, request.OrderBy);

		if (!options.IsValid(config)) ThrowError(ErrorsStrings.Values.InvalidQuery);

		var response = await projection.GridifyAsync(options, cancellationToken, config);

		var page = options.Page;
		var items = response.Data;
		var count = items.Count();
		var totalCount = response.Count;
		var totalPages = (int)Math.Ceiling((double)totalCount / (options.PageSize > 0 ? options.PageSize : 1));

		return new PageResult<TListResponse>(page, count, totalPages, totalCount, items);

	}

	public override async Task<Void> HandleAsync(TListRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var projection = Map.FromEntity(query);

		var response = await ListAsync(projection, request, cancellationToken);

		return await Send.OkAsync(response, cancellationToken);

	}

}
