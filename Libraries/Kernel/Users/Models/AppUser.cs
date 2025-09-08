namespace Olympus.Kernel;

public class AppUser {

	public Guid Id { get; internal init; } = Guid.Empty;

	public string Name { get; internal init; } = string.Empty;

	public string Email { get; internal init; } = string.Empty;

	public string? JobTitle { get; internal init; }

	public string? Department { get; internal init; }

	public string? OfficeLocation { get; internal init; }

	public string? Country { get; internal init; }

	public byte[]? Photo { get; internal init; }

}
