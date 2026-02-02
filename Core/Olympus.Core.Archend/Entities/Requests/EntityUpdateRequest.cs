namespace Olympus.Core.Archend.Entities;

public abstract record EntityUpdateRequest : IEntityUpdateRequest {

	[JsonPropertyOrder(-9999)]
	[QueryParam(IsRequired = true)]
	public required Guid Id { get; init; }

	[JsonPropertyOrder(9998)]
	[FromHeader(HeaderName = HttpHeaders.IfMatch, IsRequired = true)]
	public string ETag { get; init; } = string.Empty;

	[JsonPropertyOrder(9999)]
	public bool IsActive { get; set; } = true;

}
