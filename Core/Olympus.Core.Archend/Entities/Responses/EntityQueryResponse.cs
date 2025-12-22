namespace Olympus.Core.Archend.Entities;

public abstract record EntityQueryResponse : IEntityQueryResponse {

	[JsonPropertyOrder(-9999)]
	public Guid Id { get; init; }

	[JsonPropertyOrder(9993)]
	public UserLinkResponse? CreatedBy { get; init; }

	[JsonPropertyOrder(9994)]
	public DateTimeOffset? CreatedAt { get; init; }

	[JsonPropertyOrder(9995)]
	public UserLinkResponse? UpdatedBy { get; init; }

	[JsonPropertyOrder(9996)]
	public DateTimeOffset? UpdatedAt { get; init; }

	[JsonPropertyOrder(9997)]
	public bool IsActive { get; init; }

	[JsonPropertyOrder(9998)]
	public bool IsLocked { get; init; }

	[JsonPropertyOrder(9999)]
	public bool IsSystem { get; init; }

}
