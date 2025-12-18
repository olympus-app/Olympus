using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Olympus.Core.Backend.Database;

public interface IDatabaseContext {

	public const string SoftDeleteIndexAnnotation = "SoftDeletedFilter";

	public DbSet<T> Set<T>() where T : class;

	public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

	public ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

	public EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

	public EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
