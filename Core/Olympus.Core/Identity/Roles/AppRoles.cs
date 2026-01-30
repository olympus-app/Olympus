namespace Olympus.Core.Identity;

public static class AppRoles {

	public static AppRole Administrators { get; } = new() {
		Id = Guid.From(1),
		NameSelector = static () => RolesStrings.Values.AdministratorsName,
		DescriptionSelector = static () => RolesStrings.Values.AdministratorsDescription,
	};

}
