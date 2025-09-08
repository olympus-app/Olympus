namespace Olympus.Native.Host;

public static class NativeHost
{

	public static MauiApp CreateMauiApp()
	{

		var builder = MauiApp.CreateBuilder();

		builder.AddAppSettings();

		builder.AddModules();

		builder.AddServices();

		builder.UseMauiApp<App>();

		return builder.Build();

	}

}
