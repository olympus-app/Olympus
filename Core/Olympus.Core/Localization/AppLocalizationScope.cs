using System.Globalization;

namespace Olympus.Core.Localization;

public sealed class AppLocalizationScope : IDisposable {

	private readonly CultureInfo PreviousCulture;

	private readonly CultureInfo PreviousUICulture;

	private readonly CultureInfo? PreviousThreadCulture;

	private readonly CultureInfo? PreviousThreadUICulture;

	public AppLocalizationScope(CultureInfo culture) {

		PreviousCulture = CultureInfo.CurrentCulture;
		PreviousUICulture = CultureInfo.CurrentUICulture;
		PreviousThreadCulture = CultureInfo.DefaultThreadCurrentCulture;
		PreviousThreadUICulture = CultureInfo.DefaultThreadCurrentUICulture;

		if (AppCultureInfo.IsInvariant) return;

		CultureInfo.CurrentCulture = culture;
		CultureInfo.CurrentUICulture = culture;
		CultureInfo.DefaultThreadCurrentCulture = culture;
		CultureInfo.DefaultThreadCurrentUICulture = culture;

	}

	public void Dispose() {

		if (AppCultureInfo.IsInvariant) return;

		CultureInfo.CurrentCulture = PreviousCulture;
		CultureInfo.CurrentUICulture = PreviousUICulture;
		CultureInfo.DefaultThreadCurrentCulture = PreviousThreadCulture;
		CultureInfo.DefaultThreadCurrentUICulture = PreviousThreadUICulture;

	}

}
