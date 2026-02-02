namespace Olympus.Core.Archend.Entities;

public abstract record EntityReadRequest : IEntityReadRequest {

	[JsonPropertyOrder(-9999)]
	[QueryParam(IsRequired = true)]
	public required Guid Id { get; init; }

	[JsonPropertyOrder(9999)]
	[FromHeader(HeaderName = HttpHeaders.IfNoneMatch, IsRequired = false)]
	public string? ETag { get; init; }

}
