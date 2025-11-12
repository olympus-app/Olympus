using System.Globalization;

namespace Olympus.Core.Kernel.Localization;

public static class AppCulture {

	public static string DefaultCulture { get; } = "en";

	public static string[] SupportedCultures { get; } = ["en", "en-US", "es", "es-ES", "pt", "pt-BR"];

	public static CultureInfo CultureInfo { get; } = new CultureInfo(DefaultCulture);

	public static CultureInfo Invariant { get; } = CultureInfo.InvariantCulture;

}
