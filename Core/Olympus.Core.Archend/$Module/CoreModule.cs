namespace Olympus.Core.Archend;

public static class CoreModule {

	public const string CodeName = "Core";

	public const string FriendlyName = "System";

	public const string RoutesPath = "/core";

	public const int ModuleId = (int)AppModuleType.Core;

	public const AppModuleType ModuleType = AppModuleType.Core;

	public const AppModuleCategory ModuleCategory = AppModuleCategory.Infrastructure;

	public const string SettingsPath = $"{nameof(AppSettings.Modules)}:{CodeName}";

}
