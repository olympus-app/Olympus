namespace Olympus.Core.Archend.Identity;

public record TokenCreateRequest {

	public required string Name { get; set; }

	public DateTimeOffset? ExpiresAt { get; set; }

}
