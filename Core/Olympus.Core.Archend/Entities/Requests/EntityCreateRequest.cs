namespace Olympus.Core.Archend.Entities;

public abstract record EntityCreateRequest : IEntityCreateRequest {

	[JsonPropertyOrder(-9999)]
	public Guid Id { get; init; } = Guid.CreateVersion7();

	[JsonPropertyOrder(9999)]
	public bool IsActive { get; set; } = true;

}
