namespace Olympus.Core.Identity;

public class AppUser {

	public Guid Id { get; internal init; } = Guid.Empty;

	public string Name { get; internal init; } = string.Empty;

	public string UserName { get; internal init; } = string.Empty;

	public string Email { get; internal init; } = string.Empty;

	public string Title { get; internal init; } = string.Empty;

}
