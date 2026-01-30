namespace Olympus.Core.Archend.Storage;

public abstract record EntityWithImageLinkRequest : EntityWithStorageLinkRequest {

	[JsonPropertyOrder(9998)]
	[QueryParam(IsRequired = false)]
	public ThumbnailSize? Size { get; set; }

}
