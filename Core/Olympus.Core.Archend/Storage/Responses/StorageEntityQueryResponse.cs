namespace Olympus.Core.Archend.Storage;

public abstract record StorageEntityQueryResponse : EntityQueryResponse, IStorageEntityQueryResponse {

	[JsonPropertyOrder(-9997)]
	public string Name { get; init; } = string.Empty;

	[JsonPropertyOrder(-9998)]
	public string ContentType { get; init; } = string.Empty;

	[JsonPropertyOrder(-9999)]
	public string Extension { get; init; } = string.Empty;

	[JsonPropertyOrder(9999)]
	public long Size { get; init; }

}
