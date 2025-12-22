namespace Olympus.Core.Settings;

public class AdminSettings {

	public const string SectionName = "Admin";

	public Guid Id { get; } = AppUsers.Admin.Id;

	public string Name { get; set; } = "Admin";

	public string UserName { get; set; } = "admin@user.com";

	public string Email { get; set; } = "admin@user.com";

	public string Title { get; set; } = AppUsers.Admin.Title;

	public string Password { get; set; } = string.Empty;

}
