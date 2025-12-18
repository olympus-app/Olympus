namespace Olympus.Api.Host.Services;

public static class SettingsRegistrator {

	public static void AddSettings(this WebApplicationBuilder builder) {

		builder.Configuration.LoadSettings();

		builder.Services.AddSettings();

	}

}
