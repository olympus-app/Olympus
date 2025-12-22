using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Olympus.Core.Backend.Entities;

public interface IEntityDatabase {

	public const string SoftDeleteIndexAnnotation = "SoftDeletedFilter";

	public DbSet<T> Set<T>() where T : class;

	public EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
