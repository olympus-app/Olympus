using System.Globalization;

namespace Olympus.Core.Localization;

public static class AppCultureInfo {

	public static readonly bool IsInvariant = AppContext.TryGetSwitch("System.Globalization.Invariant", out var isInvariant) && isInvariant;

	public static CultureInfo Current => CultureInfo.CurrentCulture;

	public static CultureInfo Invariant => CultureInfo.InvariantCulture;

}
