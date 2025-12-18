namespace Olympus.Core.Archend.Identity;

public record UserUpdateRequest : EntityUpdateRequest {

	public required string Name { get; set; }

	public required string Email { get; set; }

	public string? JobTitle { get; set; }

	public string? Department { get; set; }

	public string? OfficeLocation { get; set; }

	public string? Country { get; set; }

}
