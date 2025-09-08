using EFCoreSecondLevelCacheInterceptor;

namespace Olympus.Core.Backend;

public abstract class EntityService<TEntity>(EntitiesDatabase database, bool cacheable = false) : IEntityService<TEntity> where TEntity : class, IEntity {

	public async Task<IQueryable<TEntity>> QueryAsync() {

		var entities = database.Set<TEntity>().AsNoTracking();

		if (cacheable) entities = (await entities.Cacheable(CacheExpirationMode.Absolute, 24.Hours()).ToListAsync()).AsQueryable();

		return entities;

	}

	public async Task<IEnumerable<TEntity>> GetAsync() {

		var entities = await QueryAsync();

		var result = await entities.ToListAsync();

		return result;

	}

	public async Task<IQueryable<TEntity>> QueryAsync(Guid id) {

		var entity = database.Set<TEntity>().AsNoTracking().Where(x => x.Id == id);

		if (cacheable) entity = (await entity.Cacheable(CacheExpirationMode.Absolute, 24.Hours()).ToListAsync()).AsQueryable();

		return entity;

	}

	public async Task<TEntity> GetAsync(Guid id) {

		var entity = await QueryAsync(id);

		var result = entity.SingleOrDefault() ?? throw new EntityNotFoundException<TEntity>(id);

		return result;

	}

	public async Task<TEntity> CreateAsync(TEntity input) {

		database.Set<TEntity>().Add(input);

		await database.SaveChangesAsync();

		return input;

	}

	public async Task<TEntity> UpdateAsync(TEntity input) {

		database.Set<TEntity>().Update(input);

		await database.SaveChangesAsync();

		return input;

	}

	public async Task DeleteAsync(TEntity entity) {

		database.Set<TEntity>().Remove(entity);

		await database.SaveChangesAsync();

	}

}
