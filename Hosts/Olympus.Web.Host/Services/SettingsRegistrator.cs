namespace Olympus.Web.Host.Services;

public static class SettingsRegistrator {

	public static void AddSettings(this WebAssemblyHostBuilder builder) {

		builder.Configuration.LoadSettings();

		builder.Services.AddSettings();

	}

}
