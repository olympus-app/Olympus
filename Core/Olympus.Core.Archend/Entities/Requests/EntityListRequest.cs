namespace Olympus.Core.Archend.Entities;

public abstract record EntityListRequest : IEntityListRequest {

	[HideFromDocs]
	[JsonPropertyOrder(9999)]
	[FromHeader(HeaderName = HttpHeaders.IfNoneMatch, IsRequired = false)]
	public string? ETag { get; init; }

}
