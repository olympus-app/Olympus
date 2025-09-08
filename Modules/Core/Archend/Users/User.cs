namespace Olympus.Core.Archend;

public partial class User : Entity {

	public override required Guid Id { get; set; } = Guid.CreateVersion7();

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
