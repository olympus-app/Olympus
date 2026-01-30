namespace Olympus.Core.Archend.Identity;

public record TokenListResponse : EntityListResponse {

	public string Name { get; set; } = string.Empty;

	public string Value { get; set; } = string.Empty;

	public DateTimeOffset? ExpiresAt { get; set; }

	public bool IsExpired => ExpiresAt < DateTimeOffset.UtcNow;

	public List<string> Permissions { get; set; } = [];

}
