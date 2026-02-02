namespace Olympus.Core.Archend.Entities;

public abstract record EntityDeleteRequest : IEntityDeleteRequest {

	[JsonPropertyOrder(-9999)]
	[QueryParam(IsRequired = true)]
	public required Guid Id { get; init; }

	[JsonPropertyOrder(9998)]
	[FromHeader(HeaderName = HttpHeaders.IfMatch, IsRequired = true)]
	public string ETag { get; init; } = string.Empty;

	[JsonPropertyOrder(9999)]
	[QueryParam(IsRequired = false)]
	public bool Force { get; init; }

}
