namespace Olympus.Core.Archend.Storage;

public abstract record FileLinkResponse : EntityLinkResponse, IFileLinkResponse {

	[JsonPropertyOrder(-9998)]
	public string Name { get; init; } = string.Empty;

	[JsonPropertyOrder(-9997)]
	public string ContentType { get; init; } = string.Empty;

}
