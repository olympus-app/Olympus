namespace Olympus.Core.Archend.Storage;

public abstract record FileDownloadRequest : IFileDownloadRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public Guid Id { get; init; }

}
