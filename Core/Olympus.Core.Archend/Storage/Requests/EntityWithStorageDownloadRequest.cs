namespace Olympus.Core.Archend.Storage;

public abstract record EntityWithStorageDownloadRequest : IEntityWithStorageDownloadRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public required Guid Id { get; init; }

	[JsonPropertyOrder(9999)]
	[FromHeader(HeaderName = HttpHeaders.IfNoneMatch, IsRequired = false)]
	public string? ETag { get; init; }

}
