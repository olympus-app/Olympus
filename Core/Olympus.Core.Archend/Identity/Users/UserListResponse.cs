namespace Olympus.Core.Archend.Identity;

public record UserListResponse : EntityListResponse {

	public string Name { get; init; } = AppUsers.Unknown.Name;

	public string? Email { get; init; }

	public string? JobTitle { get; init; }

	public string? Department { get; init; }

	public string? OfficeLocation { get; init; }

	public string? Country { get; init; }

}
