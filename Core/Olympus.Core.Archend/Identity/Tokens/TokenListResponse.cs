namespace Olympus.Core.Archend.Identity;

public record TokenListResponse {

	public Guid Id { get; set; }

	public required string Name { get; set; }

	public string Value { get; set; } = string.Empty;

	public DateTimeOffset? ExpiresAt { get; set; }

	public bool IsExpired => ExpiresAt < DateTimeOffset.UtcNow;

	public List<string> Permissions { get; set; } = [];

}
