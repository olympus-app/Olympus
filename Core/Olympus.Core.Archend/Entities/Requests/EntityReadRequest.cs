namespace Olympus.Core.Archend.Entities;

public abstract record EntityReadRequest : IEntityReadRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public Guid Id { get; init; }

	[JsonPropertyOrder(9999)]
	[QueryParam(IsRequired = false)]
	public Guid? RowVersion { get; init; }

}
