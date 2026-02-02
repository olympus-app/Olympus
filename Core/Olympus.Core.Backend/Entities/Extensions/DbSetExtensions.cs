using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Olympus.Core.Backend.Entities;

public static class DbSetExtensions {

	extension<TEntity>(DbSet<TEntity> dbSet) where TEntity : class {

		public EntityEntry<TEntity> Update(TEntity entity, bool checkState = false) {

			if (!checkState) return dbSet.Update(entity);

			var entry = dbSet.Entry(entity);

			if (entry.State == EntityState.Detached) return dbSet.Update(entity);

			return entry;

		}

	}

}
