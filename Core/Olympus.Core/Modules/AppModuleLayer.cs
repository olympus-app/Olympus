namespace Olympus.Core.Modules;

public abstract class AppModuleLayer(AppModuleType type, AppModuleCategory category) : IAppModuleLayer {

	public virtual int ModuleId { get; } = (int)type;

	public virtual string ModuleName { get; } = type.Name;

	public AppModuleType ModuleType { get; } = type;

	public AppModuleCategory ModuleCategory { get; } = category;

	public virtual void AddLayerServices(IServiceCollection services) { }

}
