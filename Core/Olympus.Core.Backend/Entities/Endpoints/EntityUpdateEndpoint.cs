namespace Olympus.Core.Backend.Entities;

public abstract class EntityUpdateEndpoint<TEntity, TUpdateRequest, TReadResponse, TMapper> : EntityEndpoint<TEntity, TUpdateRequest, TReadResponse, TMapper> where TEntity : class, IEntity where TUpdateRequest : class, IEntityUpdateRequest where TReadResponse : class, IEntityReadResponse where TMapper : class, IEntityUpdateMapper<TEntity, TUpdateRequest, TReadResponse> {

	protected virtual IQueryable<TEntity> Query(TUpdateRequest request) {

		return Database.Set<TEntity>().AsTracking().DefaultFilter(request.Id);

	}

	protected virtual Task<TEntity?> ReadAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default) {

		return query.SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual bool ConflictCheck(TUpdateRequest request, TEntity entity) {

		return !EntityTag.IfMatch(request.ETag, entity.ETag);

	}

	protected virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {

		Database.Set<TEntity>().Update(entity);

		await Database.SaveChangesAsync(cancellationToken);

		entity.UpdatedBy = User.CurrentUser;
		entity.CreatedBy ??= entity.UpdatedBy;

		return entity;

	}

	protected virtual void PrepareResponse(TUpdateRequest request, TReadResponse response) {

		if (response.ETag is not null) HttpContext.Response.Headers.ETag = EntityTag.From(response.ETag);

	}

	public override async Task<Void> HandleAsync(TUpdateRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var entity = await ReadAsync(query, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		var conflict = ConflictCheck(request, entity);

		if (conflict) return await Send.ConflictAsync(cancellationToken);

		Map.UpdateEntity(request, entity);

		entity = await UpdateAsync(entity, cancellationToken);

		var response = Map.FromEntity(entity);

		PrepareResponse(request, response);

		return await Send.UpdatedAsync(response, cancellationToken);

	}

}
