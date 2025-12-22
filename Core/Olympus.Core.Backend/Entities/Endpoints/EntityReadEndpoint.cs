namespace Olympus.Core.Backend.Entities;

public abstract class EntityReadEndpoint<TEntity, TReadRequest, TReadResponse, TMapper> : EntityEndpoint<TEntity, TReadRequest, TReadResponse, TMapper> where TEntity : class, IEntity where TReadRequest : class, IEntityReadRequest where TReadResponse : class, IEntityReadResponse where TMapper : class, IEntityReadMapper<TEntity, TReadResponse> {

	protected virtual IQueryable<TEntity> Query(TReadRequest request) {

		return Database.Set<TEntity>().AsNoTracking().DefaultFilter(request.Id);

	}

	protected virtual Task<TReadResponse?> ReadAsync(IQueryable<TReadResponse> projection, TReadRequest request, CancellationToken cancellationToken = default) {

		return projection.SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual void PrepareResponse(TReadRequest request, TReadResponse response) {

		if (response.ETag is not null) HttpContext.Response.Headers.ETag = EntityTag.From(response.ETag);

	}

	protected virtual bool NotModifiedCheck(TReadRequest request, TReadResponse response) {

		return EntityTag.IfNoneMatch(request.ETag, response.ETag);

	}

	public override async Task<Void> HandleAsync(TReadRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var projection = Map.FromEntity(query);

		var response = await ReadAsync(projection, request, cancellationToken);

		if (response is null) return await Send.NotFoundAsync(cancellationToken);

		PrepareResponse(request, response);

		var notModified = NotModifiedCheck(request, response);

		if (notModified) return await Send.NotModifiedAsync(cancellationToken);

		return await Send.OkAsync(response, cancellationToken);

	}

}
