namespace Olympus.Core.Archend.Entities;

public abstract record EntityUpdateRequest : IEntityUpdateRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public Guid Id { get; init; }

	[JsonPropertyOrder(9998)]
	public Guid? RowVersion { get; init; }

	[JsonPropertyOrder(9999)]
	public bool IsActive { get; set; } = true;

}
