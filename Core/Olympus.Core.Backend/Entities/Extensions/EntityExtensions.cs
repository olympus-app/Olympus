using System.Linq.Expressions;

namespace Olympus.Core.Backend.Entities;

public static class EntityExtensions {

	extension<TEntity>(IQueryable<TEntity> query) where TEntity : class, IEntity {

		public IQueryable<TEntity> DefaultFilter(Expression<Func<TEntity, bool>> predicate, bool? active = null, bool? deleted = false, bool? hidden = false, bool? locked = null, bool? system = null) {

			query = query.Where(entity =>
				(active == null || entity.IsActive == active) &&
				(deleted == null || entity.IsDeleted == deleted) &&
				(hidden == null || entity.IsHidden == hidden) &&
				(locked == null || entity.IsLocked == locked) &&
				(system == null || entity.IsSystem == system)
			);

			if (predicate is not null) query = query.Where(predicate);

			return query;

		}

		public IQueryable<TEntity> DefaultFilter(Guid? id = null, bool? active = null, bool? deleted = false, bool? hidden = false, bool? locked = null, bool? system = null) {

			return query.Where(entity =>
				(id == null || entity.Id == id) &&
				(active == null || entity.IsActive == active) &&
				(deleted == null || entity.IsDeleted == deleted) &&
				(hidden == null || entity.IsHidden == hidden) &&
				(locked == null || entity.IsLocked == locked) &&
				(system == null || entity.IsSystem == system)
			);

		}

		public IOrderedQueryable<TEntity> DefaultOrderBy() {

			return query.OrderBy(entity => entity.Id);

		}

	}

}
