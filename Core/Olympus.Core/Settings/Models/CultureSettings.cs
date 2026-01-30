namespace Olympus.Core.Settings;

public class CultureSettings : Settings {

	public string DefaultCulture { get; set; } = "pt-BR";

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Culture", this);

	}

}
