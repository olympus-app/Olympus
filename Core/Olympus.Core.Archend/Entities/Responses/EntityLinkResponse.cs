namespace Olympus.Core.Archend.Entities;

public abstract record EntityLinkResponse : IEntityLinkResponse {

	[JsonPropertyOrder(-9999)]
	public Guid Id { get; init; }

}
