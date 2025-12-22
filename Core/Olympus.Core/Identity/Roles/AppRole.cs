namespace Olympus.Core.Identity;

public class AppRole {

	public Guid Id { get; internal init; } = Guid.Empty;

	public string Name { get; internal init; } = string.Empty;

	public string Description { get; internal init; } = string.Empty;

}
