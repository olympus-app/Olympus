namespace Olympus.Core.Identity;

public class AppRole {

	public Guid Id { get; internal init; } = Guid.Empty;

	public string Name => NameSelector();

	public string Description => DescriptionSelector();

	internal Func<string> NameSelector { get; init; } = static () => string.Empty;

	internal Func<string> DescriptionSelector { get; init; } = static () => string.Empty;

}
