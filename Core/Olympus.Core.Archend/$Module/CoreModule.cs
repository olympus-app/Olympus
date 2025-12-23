namespace Olympus.Core.Archend;

public class CoreModule() : AppModule(AppModuleType.Core, AppModuleCategory.Infrastructure) {

	public const int Id = (int)AppModuleType.Core;

	public const string Name = nameof(AppModuleType.Core);

	public const AppModuleType Type = AppModuleType.Core;

	public const AppModuleCategory Category = AppModuleCategory.Infrastructure;

	public const string Route = "/core";

}
