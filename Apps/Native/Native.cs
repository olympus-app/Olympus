using Microsoft.Extensions.Logging;

namespace Olympus.Native;

public static class NativeCore
{

	public static void AddServices(this MauiAppBuilder builder)
	{

		builder.Services.AddSingleton<IFormFactor, FormFactor>();

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();

		builder.Services.AddLogging(logging => logging.AddDebug());
#endif

		builder.AddRadzenBlazor();

		builder.ConfigureFonts(fonts =>
		{
			fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		});

	}

}
