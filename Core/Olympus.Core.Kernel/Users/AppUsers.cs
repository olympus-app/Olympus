namespace Olympus.Core.Kernel.Users;

public static class AppUsers {

	public static AppUser Unknown { get; private set; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
		Name = "Unknown User",
		Email = "unknown@user.com",
	};

	public static AppUser External { get; private set; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
		Name = "External User",
		Email = "external@user.com",
	};

	public static AppUser Service { get; private set; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
		Name = "Service User",
		Email = "service@user.com",
	};

	public static AppUser Agent { get; private set; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
		Name = "Agent User",
		Email = "agent@user.com",
	};

	public static AppUser System { get; private set; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
		Name = "System User",
		Email = "system@user.com",
	};

}
