namespace Olympus.Core.Modules;

public interface IAppModuleRoutes {

	public int ModuleId { get; }

	public string ModuleName { get; }

	public AppModuleType ModuleType { get; }

	public AppModuleCategory ModuleCategory { get; }

}
