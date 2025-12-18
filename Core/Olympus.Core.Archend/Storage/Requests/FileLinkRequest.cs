namespace Olympus.Core.Archend.Storage;

public abstract record FileLinkRequest : IFileLinkRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public Guid Id { get; init; }

}
