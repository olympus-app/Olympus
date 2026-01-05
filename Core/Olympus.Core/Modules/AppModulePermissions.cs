namespace Olympus.Core.Modules;

public abstract class AppModulePermissions(AppModuleType type, AppModuleCategory category) : IAppModulePermissions {

	public virtual int ModuleId { get; } = (int)type;

	public virtual string ModuleName { get; } = type.Name;

	public AppModuleType ModuleType { get; } = type;

	public AppModuleCategory ModuleCategory { get; } = category;

}
