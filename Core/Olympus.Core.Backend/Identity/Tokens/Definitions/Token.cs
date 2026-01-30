namespace Olympus.Core.Backend.Identity;

public class Token : Entity {

	public Guid? UserId { get; set; }

	public string? Name { get; set; }

	public string? Hash { get; set; }

	public string? Value { get; set; }

	public DateTimeOffset? ExpiresAt { get; set; }

	public bool IsExpired => ExpiresAt < DateTimeOffset.UtcNow;

}
