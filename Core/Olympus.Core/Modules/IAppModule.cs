namespace Olympus.Core.Modules;

public interface IAppModule {

	public int ModuleId { get; }

	public string ModuleName { get; }

	public AppModuleType ModuleType { get; }

	public AppModuleCategory ModuleCategory { get; }

	public string ModuleRoute { get; }

	public int[] ApiVersions { get; }

}
