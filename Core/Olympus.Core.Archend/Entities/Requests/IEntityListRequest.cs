namespace Olympus.Core.Archend.Entities;

public interface IEntityListRequest : IEntityRequest {

	public string? ETag { get; init; }

}
