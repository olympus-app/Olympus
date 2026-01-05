namespace Olympus.Core.Modules;

public abstract class AppModule(AppModuleType type, AppModuleCategory category) : IAppModule {

	public virtual int ModuleId { get; } = (int)type;

	public virtual string ModuleName { get; } = type.Name;

	public AppModuleType ModuleType { get; } = type;

	public AppModuleCategory ModuleCategory { get; } = category;

	public virtual string ModuleRoute { get; } = "/" + type.Name.ToLower();

	public virtual int[] ApiVersions { get; } = [1];

}
