using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Olympus.Api;

internal static class LocalizationConfiguration {

	internal static void AddLocalization(this WebApplicationBuilder builder) {

		builder.Services.AddLocalization();

		CultureInfo.DefaultThreadCurrentCulture = AppLocalization.CultureInfo;
		CultureInfo.DefaultThreadCurrentUICulture = AppLocalization.CultureInfo;

		builder.Services.AddSingleton<IStringLocalizerFactory, AppStringLocalizerFactory>();

	}

	internal static void UseLocalization(this WebApplication app) {

		app.UseRequestLocalization(options => {
			options.SetDefaultCulture(AppLocalization.DefaultCulture);
			options.AddSupportedCultures(AppLocalization.SupportedCultures);
			options.AddSupportedUICultures(AppLocalization.SupportedCultures);
		});

	}

}
