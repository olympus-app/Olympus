namespace Olympus.Web.Host.Services;

public static partial class SettingsRegistrator {

	[GenerateServiceRegistrations(AssignableTo = typeof(ISettings), CustomHandler = nameof(AddSettingsModel), AssemblyNameFilter = $"{AppSettings.AppBaseName}.*")]
	private static partial void AddSettingsModels(this WebAssemblyHostBuilder builder);

	private static void AddSettingsModel<TSetting>(this WebAssemblyHostBuilder builder) where TSetting : class, ISettings {

		Settings.AddSetting<TSetting>(builder.Services, builder.Configuration, typeof(SettingsRegistrator).Assembly);

	}

	public static void AddSettings(this WebAssemblyHostBuilder builder) {

		builder.AddSettingsModels();

	}

}
