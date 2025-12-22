namespace Olympus.Core.Archend.Entities;

public abstract record EntityReadRequest : IEntityReadRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public Guid Id { get; init; }

	[HideFromDocs]
	[JsonPropertyOrder(9999)]
	[FromHeader(HeaderName = Headers.IfNoneMatch, IsRequired = false)]
	public string? ETag { get; init; }

}
