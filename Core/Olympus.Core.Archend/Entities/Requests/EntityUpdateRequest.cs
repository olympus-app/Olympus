namespace Olympus.Core.Archend.Entities;

public abstract record EntityUpdateRequest : IEntityUpdateRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public Guid Id { get; init; }

	[HideFromDocs]
	[JsonPropertyOrder(9998)]
	[FromHeader(HeaderName = Headers.IfMatch, IsRequired = false)]
	public string ETag { get; init; } = "*";

	[JsonPropertyOrder(9999)]
	public bool IsActive { get; set; } = true;

}
