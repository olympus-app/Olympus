using Microsoft.Extensions.Logging;
using Olympus.Frontend.Native.Services;
using Olympus.Frontend.Interface.Services;
using Radzen;

namespace Olympus.Frontend.Native;

public static class MauiProgram {

	public static MauiApp CreateMauiApp() {

		var builder = MauiApp.CreateBuilder();

		builder
		.UseMauiApp<App>()
		.ConfigureFonts(fonts => {
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		});

		builder.Services.AddSingleton<IFormFactor, FormFactor>();

		builder.Services.AddMauiBlazorWebView();

		builder.Services.AddRadzenComponents();

#if DEBUG

		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();

#endif

		return builder.Build();

	}

}
