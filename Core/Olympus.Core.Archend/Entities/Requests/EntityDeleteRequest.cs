namespace Olympus.Core.Archend.Entities;

public abstract record EntityDeleteRequest : IEntityDeleteRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public Guid Id { get; init; }

	[JsonPropertyOrder(9998)]
	[QueryParam(IsRequired = false)]
	public Guid? RowVersion { get; init; }

	[JsonPropertyOrder(9999)]
	[QueryParam(IsRequired = false)]
	public bool Force { get; init; }

}
