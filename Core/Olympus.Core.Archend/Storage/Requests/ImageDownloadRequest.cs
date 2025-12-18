namespace Olympus.Core.Archend.Storage;

public record ImageDownloadRequest : FileDownloadRequest {

	[JsonPropertyOrder(9999)]
	[QueryParam(IsRequired = false)]
	public ThumbnailSize? Size { get; set; }

}
