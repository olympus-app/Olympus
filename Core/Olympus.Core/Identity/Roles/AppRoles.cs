namespace Olympus.Core.Identity;

public static class AppRoles {

	public static AppRole Administrators { get; } = new() {
		Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
		Name = RolesStrings.Values.Administrators,
	};

}
