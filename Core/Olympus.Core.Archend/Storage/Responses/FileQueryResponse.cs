namespace Olympus.Core.Archend.Storage;

public abstract record FileQueryResponse : EntityQueryResponse, IFileQueryResponse {

	[JsonPropertyOrder(-9998)]
	public string Name { get; init; } = string.Empty;

	[JsonPropertyOrder(-9997)]
	public string Extension { get; init; } = string.Empty;

	[JsonPropertyOrder(-9996)]
	public string ContentType { get; init; } = string.Empty;

	[JsonPropertyOrder(9999)]
	public long Size { get; init; }

}
