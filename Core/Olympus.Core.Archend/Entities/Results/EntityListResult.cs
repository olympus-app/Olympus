namespace Olympus.Core.Archend.Entities;

public class EntityListResult<TResponse>(IEnumerable<TResponse> items) : ListResult<TResponse>(items), IEntityResponse where TResponse : class, IEntityListResponse { }
