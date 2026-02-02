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

		return Query(tracking).ToListAsync(cancellationToken);

	}

	public virtual Task<TEntity?> ReadAsync(Guid id, bool tracking = false, CancellationToken cancellationToken = default) {

		return Query(id, tracking).SingleOrDefaultAsync(cancellationToken);

	}

	public virtual Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default) {

		Database.Set<TEntity>().Add(entity);

		return Task.FromResult(entity);

	}

	public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) {

		Database.Set<TEntity>().Update(entity, true);

		return Task.FromResult(entity);

	}

	public virtual Task<TEntity?> DeleteAsync(TEntity entity, bool force, CancellationToken cancellationToken = default) {

		if (force) {

			Database.Set<TEntity>().Remove(entity);

			return Task.FromResult((TEntity?)null);

		}

		entity.IsDeleted = true;

		Database.Set<TEntity>().Update(entity, true);

		return Task.FromResult((TEntity?)entity);

	}

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {

		return Database.SaveChangesAsync(cancellationToken);

	}

}
