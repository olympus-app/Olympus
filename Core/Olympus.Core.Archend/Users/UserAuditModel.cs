namespace Olympus.Core.Archend.Users;

public partial record UserAuditModel {

	public required Guid Id { get; init; }

	public required string Name { get; init; }

	public required string Email { get; init; }

	public string? JobTitle { get; init; }

	public string? Department { get; init; }

	public byte[]? Photo { get; init; }

}
