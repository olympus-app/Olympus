namespace Olympus.Core.Archend.Entities;

public abstract record EntityDeleteRequest : IEntityDeleteRequest {

	[JsonPropertyOrder(-9999)]
	[QueryParam(IsRequired = true)]
	public required Guid Id { get; init; }

	[HideFromDocs]
	[JsonPropertyOrder(9998)]
	[FromHeader(HeaderName = HttpHeaders.IfMatch, IsRequired = false)]
	public string ETag { get; init; } = "*";

	[JsonPropertyOrder(9999)]
	[QueryParam(IsRequired = false)]
	public bool Force { get; init; }

}
