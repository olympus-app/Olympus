namespace Olympus.Core.Archend.Storage;

public abstract record EntityWithStorageLinkRequest : IEntityWithStorageLinkRequest {

	[JsonPropertyOrder(-9999)]
	[RouteParam(IsRequired = true)]
	public required Guid Id { get; init; }

}
