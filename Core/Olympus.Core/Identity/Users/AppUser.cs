namespace Olympus.Core.Identity;

public class AppUser {

	public Guid Id { get; internal init; } = Guid.Empty;

	public string Name => NameSelector();

	public string UserName => UserNameSelector();

	public string Email => EmailSelector();

	public string Title => TitleSelector();

	internal Func<string> NameSelector { get; init; } = static () => string.Empty;

	internal Func<string> UserNameSelector { get; init; } = static () => string.Empty;

	internal Func<string> EmailSelector { get; init; } = static () => string.Empty;

	internal Func<string> TitleSelector { get; init; } = static () => string.Empty;

}
