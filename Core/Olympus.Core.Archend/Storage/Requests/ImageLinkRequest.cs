namespace Olympus.Core.Archend.Storage;

public abstract record ImageLinkRequest : FileLinkRequest {

	[JsonPropertyOrder(9999)]
	[QueryParam(IsRequired = false)]
	public ThumbnailSize? Size { get; set; }

}
