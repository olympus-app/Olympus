namespace Olympus.Core.Kernel.Users;

public interface IAppUser {

	public Guid Id { get; }

	public string Name { get; }

	public string Email { get; }

	public string? JobTitle { get; }

	public string? Department { get; }

	public string? OfficeLocation { get; }

	public string? Country { get; }

	public byte[]? Photo { get; }

}
