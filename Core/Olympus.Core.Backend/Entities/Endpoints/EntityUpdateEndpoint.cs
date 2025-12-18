namespace Olympus.Core.Backend.Entities;

public abstract class EntityUpdateEndpoint<TEntity, TUpdateRequest, TReadResponse, TMapper> : EntityEndpoint<TEntity, TUpdateRequest, TReadResponse, TMapper> where TEntity : class, IEntity where TUpdateRequest : class, IEntityUpdateRequest where TReadResponse : class, IEntityReadResponse where TMapper : class, IEntityUpdateMapper<TEntity, TUpdateRequest, TReadResponse> {

	protected virtual IQueryable<TEntity> Query(TUpdateRequest request) {

		return Database.Set<TEntity>().AsTracking().DefaultFilter(request.Id);

	}

	protected virtual Task<TEntity?> ReadAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default) {

		return query.SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual bool ConflictCheck(TEntity entity, TUpdateRequest request) {

		return EntityTag.NotMatch(entity?.RowVersion, request?.RowVersion);

	}

	protected virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {

		await Database.SaveChangesAsync(cancellationToken);

		entity.UpdatedBy = User.CurrentUser;
		entity.CreatedBy ??= entity.UpdatedBy;

		return entity;

	}

	public override async Task<Void> HandleAsync(TUpdateRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var entity = await ReadAsync(query, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		var conflict = ConflictCheck(entity, request);

		if (conflict) return await Send.ConflictAsync(cancellationToken);

		Map.UpdateEntity(request, entity);

		entity = await UpdateAsync(entity, cancellationToken);

		var response = Map.FromEntity(entity);

		return await Send.UpdatedAsync(response, cancellationToken);

	}

}
