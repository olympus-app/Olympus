namespace Olympus.Core.Archend.Entities;

public interface IEntityListRequest : IEntityRequest {

	public int Page { get; init; }

	public int PageSize { get; init; }

	public string Filter { get; init; }

	public string OrderBy { get; init; }

}
