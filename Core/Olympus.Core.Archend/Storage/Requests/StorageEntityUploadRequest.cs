namespace Olympus.Core.Archend.Storage;

public abstract record StorageEntityUploadRequest : IStorageEntityUploadRequest {

	[JsonPropertyOrder(-9999)]
	[QueryParam(IsRequired = true)]
	public required Guid Id { get; init; }

	[JsonPropertyOrder(9999)]
	[FromHeader(HeaderName = HttpHeaders.IfMatch, IsRequired = true)]
	public string ETag { get; init; } = string.Empty;

}
