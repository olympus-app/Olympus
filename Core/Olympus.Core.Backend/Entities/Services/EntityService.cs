using System.Security.Claims;

namespace Olympus.Core.Backend.Entities;

public abstract class EntityService<TEntity>(IDatabaseService database, IHttpContextAccessor accessor) : IEntityService<TEntity> where TEntity : class, IEntity {

	protected IDatabaseService Database { get; } = database;

	protected ClaimsPrincipal User => accessor.HttpContext?.User ?? AppClaimsPrincipal.Anonymous;

	public virtual IQueryable<TEntity> Query(bool tracking = false) {

		var query = Database.Set<TEntity>().AsQueryable();

		if (!tracking) query = query.AsNoTracking();

		return query.DefaultFilter().DefaultOrderBy();

	}

	public virtual IQueryable<TEntity> Query(Guid id, bool tracking = false) {

		var query = Database.Set<TEntity>().AsQueryable();

		if (!tracking) query = query.AsNoTracking();

		return query.DefaultFilter(id);

	}

	public virtual Task<List<TEntity>> ListAsync(bool tracking = false, CancellationToken cancellationToken = default) {

		return Query(tracking).ListAsync(cancellationToken);

	}

	public virtual Task<TEntity?> ReadAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default) {

		return Query(id, tracking).ReadAsync(cancellationToken);

	}

	public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default) {

		Database.Set<TEntity>().Add(entity);

		await Database.SaveChangesAsync(cancellationToken);

		entity.CreatedBy = User.AsEntity();
		entity.UpdatedBy ??= entity.CreatedBy;

		return entity;

	}

	public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {

		Database.Set<TEntity>().Update(entity);

		await Database.SaveChangesAsync(cancellationToken);

		entity.UpdatedBy = User.AsEntity();
		entity.CreatedBy ??= entity.UpdatedBy;

		return entity;

	}

	public virtual async Task<TEntity?> DeleteAsync(TEntity entity, bool force, CancellationToken cancellationToken = default) {

		if (force) {

			Database.Set<TEntity>().Remove(entity);

			await Database.SaveChangesAsync(cancellationToken);

			return null;

		}

		entity.IsDeleted = true;

		Database.Set<TEntity>().Update(entity);

		await Database.SaveChangesAsync(cancellationToken);

		return entity;

	}

}
