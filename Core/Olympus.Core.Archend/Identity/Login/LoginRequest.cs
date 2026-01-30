namespace Olympus.Core.Archend.Identity;

public record LoginRequest : IRequest {

	public string Email { get; set; } = string.Empty;

	public string Password { get; set; } = string.Empty;

	public bool IsPersistent { get; set; } = true;

}
