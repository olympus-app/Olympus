namespace Olympus.Core.Archend.Storage;

public abstract record StorageEntityLinkResponse : EntityLinkResponse, IStorageEntityLinkResponse {

	[JsonPropertyOrder(-9998)]
	public string Name { get; init; } = string.Empty;

	[JsonPropertyOrder(-9997)]
	public string ContentType { get; init; } = string.Empty;

	[JsonPropertyOrder(9999)]
	public DateTimeOffset? UpdatedAt { get; init; }

}
