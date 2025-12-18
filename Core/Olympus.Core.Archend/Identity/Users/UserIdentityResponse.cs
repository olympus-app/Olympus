namespace Olympus.Core.Archend.Identity;

public record UserIdentityResponse {

	public Guid Id { get; init; }

	public string Name { get; init; } = AppUsers.Unknown.Name;

	public string? Email { get; init; }

	public string? UserName { get; init; }

	public string? JobTitle { get; init; }

	public string? Department { get; init; }

	public string? OfficeLocation { get; init; }

	public string? Country { get; init; }

	public List<string> Roles { get; init; } = [];

	public string? Permissions { get; init; }

}
