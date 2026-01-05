namespace Olympus.Core.Settings;

public class CompanySettings : Settings {

	public string Name { get; set; } = "Olympus";

	public string Domain { get; set; } = "olympus.app.br";

	public string WebSite { get; set; } = "https://www.olympus.app.br";

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Company", this);

	}

}
