namespace Olympus.Core.Backend.Entities;

public abstract class EntityDeleteEndpoint<TEntity, TDeleteRequest> : EntityEndpoint<TEntity, TDeleteRequest> where TEntity : class, IEntity where TDeleteRequest : class, IEntityDeleteRequest {

	protected virtual IQueryable<TEntity> Query(TDeleteRequest request) {

		return Database.Set<TEntity>().AsTracking().DefaultFilter(request.Id, deleted: request.Force ? null : false);

	}

	protected virtual Task<TEntity?> ReadAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default) {

		return query.SingleOrDefaultAsync(cancellationToken);

	}

	protected virtual bool ConflictCheck(TDeleteRequest request, TEntity entity) {

		return !EntityTag.IfMatch(request.ETag, entity.ETag);

	}

	protected virtual async Task<TEntity> SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default) {

		entity.IsDeleted = true;

		Database.Set<TEntity>().Update(entity);

		await Database.SaveChangesAsync(cancellationToken);

		return entity;

	}

	protected virtual async Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default) {

		Database.Set<TEntity>().Remove(entity);

		await Database.SaveChangesAsync(cancellationToken);

	}

	protected virtual void PrepareResponse(TDeleteRequest request, TEntity entity) {

		if (entity.ETag is not null && !request.Force) HttpContext.Response.Headers.ETag = EntityTag.From(entity.ETag);

	}

	public override async Task<Void> HandleAsync(TDeleteRequest request, CancellationToken cancellationToken) {

		var query = Query(request);

		var entity = await ReadAsync(query, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		var conflict = ConflictCheck(request, entity);

		if (conflict) return await Send.ConflictAsync(cancellationToken);

		if (request.Force) {

			await HardDeleteAsync(entity, cancellationToken);

		} else {

			entity = await SoftDeleteAsync(entity, cancellationToken);

		}

		PrepareResponse(request, entity);

		return await Send.DeletedAsync(cancellationToken);

	}

}
