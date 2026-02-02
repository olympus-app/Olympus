using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Olympus.Core.Backend.Entities;

public interface IDatabaseService {

	public const string SoftDeleteIndexAnnotation = "SoftDeletedFilter";

	public DbSet<TEntity> Set<TEntity>() where TEntity : class;

	public EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

	public bool IsAdded<TEntity>(TEntity entity) where TEntity : class;

	public bool IsModified<TEntity>(TEntity entity) where TEntity : class;

	public bool IsDeleted<TEntity>(TEntity entity) where TEntity : class;

	public bool IsUnchanged<TEntity>(TEntity entity) where TEntity : class;

	public bool IsDetached<TEntity>(TEntity entity) where TEntity : class;

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
