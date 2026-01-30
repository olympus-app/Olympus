namespace Olympus.Core.Identity;

public static class AppUsers {

	public static AppUser Unknown { get; } = new() {
		Id = Guid.From(1),
		NameSelector = static () => UsersStrings.Values.UnknownName,
		UserNameSelector = static () => UsersStrings.Values.UnknownEmail,
		EmailSelector = static () => UsersStrings.Values.UnknownEmail,
		TitleSelector = static () => UsersStrings.Values.UnknownTitle,
	};

	public static AppUser External { get; } = new() {
		Id = Guid.From(2),
		NameSelector = static () => UsersStrings.Values.ExternalName,
		UserNameSelector = static () => UsersStrings.Values.ExternalEmail,
		EmailSelector = static () => UsersStrings.Values.ExternalEmail,
		TitleSelector = static () => UsersStrings.Values.ExternalTitle,
	};

	public static AppUser Service { get; } = new() {
		Id = Guid.From(3),
		NameSelector = static () => UsersStrings.Values.ServiceName,
		UserNameSelector = static () => UsersStrings.Values.ServiceEmail,
		EmailSelector = static () => UsersStrings.Values.ServiceEmail,
		TitleSelector = static () => UsersStrings.Values.ServiceTitle,
	};

	public static AppUser Agents { get; } = new() {
		Id = Guid.From(4),
		NameSelector = static () => UsersStrings.Values.AgentsName,
		UserNameSelector = static () => UsersStrings.Values.AgentsEmail,
		EmailSelector = static () => UsersStrings.Values.AgentsEmail,
		TitleSelector = static () => UsersStrings.Values.AgentsTitle,
	};

	public static AppUser System { get; } = new() {
		Id = Guid.From(5),
		NameSelector = static () => UsersStrings.Values.SystemName,
		UserNameSelector = static () => UsersStrings.Values.SystemEmail,
		EmailSelector = static () => UsersStrings.Values.SystemEmail,
		TitleSelector = static () => UsersStrings.Values.SystemTitle,
	};

	public static AppUser Admin { get; } = new() {
		Id = Guid.From(6),
		NameSelector = static () => UsersStrings.Values.AdminName,
		EmailSelector = static () => UsersStrings.Values.AdminEmail,
		UserNameSelector = static () => UsersStrings.Values.AdminEmail,
		TitleSelector = static () => UsersStrings.Values.AdminTitle,
	};

}
