using System.Globalization;

namespace Olympus.Kernel;

public static class AppLocalization {

	public static string DefaultCulture { get; } = "en";

	public static string[] SupportedCultures { get; } = ["en", "en-US", "es", "es-ES", "pt", "pt-BR"];

	public static CultureInfo CultureInfo { get; } = new CultureInfo(DefaultCulture);

}
