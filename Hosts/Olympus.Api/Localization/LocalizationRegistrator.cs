using Microsoft.Extensions.Localization;

namespace Olympus.Api.Localization;

public static class LocalizationRegistrator {

	public static void AddLocalizationServices(this WebApplicationBuilder builder) {

		builder.Services.AddLocalization();

		builder.Services.AddSingleton<IStringLocalizerFactory, AppStringLocalizerFactory>();

	}

}
