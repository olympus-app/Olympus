namespace Olympus.Core.Identity;

public static class AppUsers {

	public static AppUser Unknown { get; } = new() {
		Id = Guid.From(1),
		Name = UsersStrings.Values.UnknownName,
		UserName = UsersStrings.Values.UnknownEmail,
		Email = UsersStrings.Values.UnknownEmail,
		Title = UsersStrings.Values.UnknownTitle,
	};

	public static AppUser External { get; } = new() {
		Id = Guid.From(2),
		Name = UsersStrings.Values.ExternalName,
		UserName = UsersStrings.Values.ExternalEmail,
		Email = UsersStrings.Values.ExternalEmail,
		Title = UsersStrings.Values.ExternalTitle,
	};

	public static AppUser Service { get; } = new() {
		Id = Guid.From(3),
		Name = UsersStrings.Values.ServiceName,
		UserName = UsersStrings.Values.ServiceEmail,
		Email = UsersStrings.Values.ServiceEmail,
		Title = UsersStrings.Values.ServiceTitle,
	};

	public static AppUser Agents { get; } = new() {
		Id = Guid.From(4),
		Name = UsersStrings.Values.AgentsName,
		UserName = UsersStrings.Values.AgentsEmail,
		Email = UsersStrings.Values.AgentsEmail,
		Title = UsersStrings.Values.AgentsTitle,
	};

	public static AppUser System { get; } = new() {
		Id = Guid.From(5),
		Name = UsersStrings.Values.SystemName,
		UserName = UsersStrings.Values.SystemEmail,
		Email = UsersStrings.Values.SystemEmail,
		Title = UsersStrings.Values.SystemTitle,
	};

	public static AppUser Admin { get; } = new() {
		Id = Guid.From(6),
		Name = UsersStrings.Values.AdminName,
		Email = UsersStrings.Values.AdminEmail,
		UserName = UsersStrings.Values.AdminEmail,
		Title = UsersStrings.Values.AdminTitle,
	};

}
