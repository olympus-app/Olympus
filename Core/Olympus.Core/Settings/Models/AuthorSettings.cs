namespace Olympus.Core.Settings;

public class AuthorSettings : Settings {

	public string Name { get; } = "Éwerton Ferreira";

	public string Email { get; set; } = "contact@olympus.app.br";

	public string Phone { get; set; } = "(00) 98765-4321";

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Author", this);

	}

}
