using System.Globalization;

namespace Olympus.Core.Localization;

public class LocalizationManager : ISingletonService {

	public event Action<CultureInfo>? CultureChanged;

	public static CultureInfo CurrentCulture => CultureInfo.CurrentUICulture;

	public void SetCulture(CultureInfo culture) {

		CultureInfo.CurrentCulture = culture;
		CultureInfo.CurrentUICulture = culture;
		CultureInfo.DefaultThreadCurrentCulture = culture;
		CultureInfo.DefaultThreadCurrentUICulture = culture;

		CultureChanged?.Invoke(culture);

	}

}
