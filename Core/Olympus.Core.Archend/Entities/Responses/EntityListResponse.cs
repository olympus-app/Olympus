namespace Olympus.Core.Archend.Entities;

public abstract record EntityListResponse : IEntityListResponse {

	[JsonPropertyOrder(-9999)]
	public Guid Id { get; init; }

	[JsonPropertyOrder(9997)]
	public bool IsActive { get; init; }

	[JsonPropertyOrder(9998)]
	public bool IsLocked { get; init; }

	[JsonPropertyOrder(9999)]
	public bool IsSystem { get; init; }

}
