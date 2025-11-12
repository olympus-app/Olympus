using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Olympus.Core.Kernel.Localization;

public static class AppLocalizationRegistrator {

	public static void AddLocalizationServices(this IServiceCollection services) {

		services.AddLocalization();
		services.AddSingleton<IStringLocalizerFactory, AppStringLocalizerFactory>();

		CultureInfo.DefaultThreadCurrentCulture = AppCulture.CultureInfo;
		CultureInfo.DefaultThreadCurrentUICulture = AppCulture.CultureInfo;

	}

}
