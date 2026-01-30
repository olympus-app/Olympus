namespace Olympus.Core.Archend.Identity;

public record TokenCreateRequest : EntityCreateRequest {

	public required string Name { get; set; }

	public DateTimeOffset? ExpiresAt { get; set; }

}
