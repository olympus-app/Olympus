namespace Olympus.Core.Archend.Entities;

public class EntityPageResult<TResponse>(int page, int pageSize, IEnumerable<TResponse> items, int totalCount) : PageResult<TResponse>(page, pageSize, items, totalCount), IEntityResponse where TResponse : class, IEntityQueryResponse { }
