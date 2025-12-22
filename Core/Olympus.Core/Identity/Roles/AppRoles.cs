namespace Olympus.Core.Identity;

public static class AppRoles {

	public static AppRole Administrators { get; } = new() {
		Id = Guid.From(1),
		Name = RolesStrings.Values.AdministratorsName,
		Description = RolesStrings.Values.AdministratorsDescription,
	};

}
