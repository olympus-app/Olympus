namespace Olympus.Core.Modules;

public interface IAppModuleLayer {

	public int ModuleId { get; }

	public string ModuleName { get; }

	public AppModuleType ModuleType { get; }

	public AppModuleCategory ModuleCategory { get; }

	public void AddLayerServices(IServiceCollection services);

}
