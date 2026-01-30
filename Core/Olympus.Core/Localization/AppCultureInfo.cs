using System.Globalization;

namespace Olympus.Core.Localization;

public static class AppCultureInfo {

	public static CultureInfo Current => CultureInfo.CurrentCulture;

	public static CultureInfo Invariant => CultureInfo.InvariantCulture;

	public static readonly bool IsInvariant = AppContext.TryGetSwitch("System.Globalization.Invariant", out var isInvariant) && isInvariant;

	public static CultureInfo English => IsInvariant ? Invariant : CultureInfo.GetCultureInfo("en");

	public static CultureInfo Portuguese => IsInvariant ? Invariant : CultureInfo.GetCultureInfo("pt");

	public static CultureInfo Spanish => IsInvariant ? Invariant : CultureInfo.GetCultureInfo("es");

	public static CultureInfo[] SupportedCultures => [English, Portuguese, Spanish];

	public static CultureInfo Get(string culture) {

		return culture.ToLowerInvariant() switch {
			"en" or "en-us" => English,
			"pt" or "pt-br" => Portuguese,
			"es" or "es-es" => Spanish,
			_ => Invariant,
		};

	}

}
