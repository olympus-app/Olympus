namespace Olympus.Core.Frontend.Services;

public abstract class EntityService<TEntity>(ApiClient api) : IEntityService {

	protected ApiClient Api { get; } = api;

}
