namespace Olympus.Zeus.Archend;

public partial record UserModel : Model {

	public override required Guid Id { get; set; }

	public required string Name { get; set; }

	public required string Email { get; set; }

	public string? Username { get; set; }

	public string? Password { get; set; }

	public string? JobTitle { get; set; }

	public string? Department { get; set; }

	public string? OfficeLocation { get; set; }

	public string? Country { get; set; }

	public byte[]? Photo { get; set; }

}
