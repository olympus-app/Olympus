namespace Olympus.Core.Archend.Identity;

public record UserCreateRequest : EntityCreateRequest {

	public required string Name { get; set; }

	public string? Email { get; set; }

	public string? Title { get; set; }

	public string? Password { get; set; }

}
