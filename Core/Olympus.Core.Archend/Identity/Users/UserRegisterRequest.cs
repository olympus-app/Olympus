namespace Olympus.Core.Archend.Identity;

public record UserRegisterRequest {

	public required string Name { get; set; }

	public required string Email { get; set; }

	public required string Password { get; set; }

}
