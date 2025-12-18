namespace Olympus.Core.Archend.Storage;

public abstract record FileDeleteRequest : IFileDeleteRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public Guid Id { get; init; }

}
