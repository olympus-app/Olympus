namespace Olympus.Core.Settings;

public class CultureSettings {

	public const string SectionName = "Culture";

	public static string[] SupportedCultures { get; } = ["en", "en-US", "es", "es-ES", "pt", "pt-BR"];

	public string DefaultCulture { get; set; } = "pt-BR";

}
