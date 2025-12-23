namespace Olympus.Core.Settings;

public class LicenseSettings : Settings {

	public string Name { get; set; } = "All Rights Reserved";

	public string Link { get; set; } = "https://olympus.app.br/license";

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("License", this);

	}

}
