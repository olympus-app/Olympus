namespace Olympus.Core.Settings;

public class CultureSettings : Settings {

	public static string[] SupportedCultures { get; } = ["en", "en-US", "es", "es-ES", "pt", "pt-BR"];

	public string DefaultCulture { get; set; } = "pt-BR";

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Culture", this);

	}

}
