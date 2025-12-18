namespace Olympus.Core.Archend.Identity;

public record UserCreateRequest : EntityCreateRequest {

	public required string Name { get; set; }

	public string? Email { get; set; }

	public string? Password { get; set; }

	public string? JobTitle { get; set; }

	public string? Department { get; set; }

	public string? OfficeLocation { get; set; }

	public string? Country { get; set; }

}
