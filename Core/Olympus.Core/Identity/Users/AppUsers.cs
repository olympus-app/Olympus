namespace Olympus.Core.Identity;

public static class AppUsers {

	public static AppUser Unknown { get; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
		Name = UsersStrings.Values.UnknownName,
		Email = UsersStrings.Values.UnknownEmail,
		UserName = UsersStrings.Values.UnknownEmail,
	};

	public static AppUser External { get; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
		Name = UsersStrings.Values.ExternalName,
		Email = UsersStrings.Values.ExternalEmail,
		UserName = UsersStrings.Values.ExternalEmail,
	};

	public static AppUser Service { get; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
		Name = UsersStrings.Values.ServiceName,
		Email = UsersStrings.Values.ServiceEmail,
		UserName = UsersStrings.Values.ServiceEmail,
	};

	public static AppUser Agents { get; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
		Name = UsersStrings.Values.AgentsName,
		Email = UsersStrings.Values.AgentsEmail,
		UserName = UsersStrings.Values.AgentsEmail,
	};

	public static AppUser System { get; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
		Name = UsersStrings.Values.SystemName,
		Email = UsersStrings.Values.SystemEmail,
		UserName = UsersStrings.Values.SystemEmail,
	};

}
