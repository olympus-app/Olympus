using System.Globalization;

namespace Olympus.Core.Localization;

public sealed class AppLocalizationScope : IDisposable {

	private readonly CultureInfo PreviousCulture;

	private readonly CultureInfo PreviousUICulture;

	public AppLocalizationScope(AppSettings settings, CultureInfo? culture = null) {

		PreviousCulture = CultureInfo.CurrentCulture;
		PreviousUICulture = CultureInfo.CurrentUICulture;

		if (AppCultureInfo.IsInvariant) return;

		culture ??= CultureInfo.GetCultureInfo(settings.Culture.DefaultCulture);

		CultureInfo.CurrentCulture = culture;
		CultureInfo.CurrentUICulture = culture;

	}

	public void Dispose() {

		if (AppCultureInfo.IsInvariant) return;

		CultureInfo.CurrentCulture = PreviousCulture;
		CultureInfo.CurrentUICulture = PreviousUICulture;

	}

}
