namespace Olympus.Core.Modules;

public abstract class AppModuleRoutes(AppModuleType type, AppModuleCategory category) : IAppModuleRoutes {

	public virtual int ModuleId { get; } = (int)type;

	public virtual string ModuleName { get; } = type.Name;

	public AppModuleType ModuleType { get; } = type;

	public AppModuleCategory ModuleCategory { get; } = category;

}
