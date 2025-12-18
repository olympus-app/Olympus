namespace Olympus.Core.Backend.Entities;

public abstract class EntityReadEndpoint<TEntity, TReadRequest, TReadResponse, TMapper> : EntityEndpoint<TEntity, TReadRequest, TReadResponse, TMapper> where TEntity : class, IEntity where TReadRequest : class, IEntityReadRequest where TReadResponse : class, IEntityReadResponse where TMapper : class, IEntityReadMapper<TEntity, TReadResponse> {

	protected virtual IQueryable<TEntity> Query(TReadRequest request) {

		return Database.Set<TEntity>().AsNoTracking().DefaultFilter(request.Id);

	}

	protected virtual Task<TReadResponse?> ReadAsync(IQueryable<TReadResponse> projection, TReadRequest request, CancellationToken cancellationToken = default) {

		return projection.SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual bool NotModifiedCheck(TReadResponse result, TReadRequest request) {

		return EntityTag.Match(result?.RowVersion, request?.RowVersion);

	}

	public override async Task<Void> HandleAsync(TReadRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var projection = Map.FromEntity(query);

		var response = await ReadAsync(projection, request, cancellationToken);

		if (response is null) return await Send.NotFoundAsync(cancellationToken);

		var notModified = NotModifiedCheck(response, request);

		if (notModified) return await Send.NotModifiedAsync(cancellationToken);

		return await Send.OkAsync(response, cancellationToken);

	}

}
