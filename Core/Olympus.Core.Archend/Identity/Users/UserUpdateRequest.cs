namespace Olympus.Core.Archend.Identity;

public record UserUpdateRequest : EntityUpdateRequest {

	public required string Name { get; set; }

	public string? Email { get; set; }

	public string? Title { get; set; }

}
