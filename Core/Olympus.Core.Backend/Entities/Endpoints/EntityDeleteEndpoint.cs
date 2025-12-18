namespace Olympus.Core.Backend.Entities;

public abstract class EntityDeleteEndpoint<TEntity, TDeleteRequest> : EntityEndpoint<TEntity, TDeleteRequest> where TEntity : class, IEntity where TDeleteRequest : class, IEntityDeleteRequest {

	protected virtual IQueryable<TEntity> Query(TDeleteRequest request) {

		return Database.Set<TEntity>().AsTracking().DefaultFilter(request.Id, deleted: request.Force ? null : false);

	}

	protected virtual Task<TEntity?> ReadAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default) {

		return query.SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual bool ConflictCheck(TEntity entity, TDeleteRequest request) {

		return EntityTag.NotMatch(entity?.RowVersion, request?.RowVersion);

	}

	protected virtual async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default) {

		entity.IsDeleted = true;

		await Database.SaveChangesAsync(cancellationToken);

	}

	protected virtual async Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default) {

		Database.Set<TEntity>().Remove(entity);

		await Database.SaveChangesAsync(cancellationToken);

	}

	public override async Task<Void> HandleAsync(TDeleteRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var entity = await ReadAsync(query, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		var conflict = ConflictCheck(entity, request);

		if (conflict) return await Send.ConflictAsync(cancellationToken);

		if (request.Force) {

			await HardDeleteAsync(entity, cancellationToken);

		} else {

			await SoftDeleteAsync(entity, cancellationToken);

		}

		return await Send.DeletedAsync(cancellationToken);

	}

}
