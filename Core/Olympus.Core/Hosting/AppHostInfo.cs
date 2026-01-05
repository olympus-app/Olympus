using System.Reflection;

namespace Olympus.Core.Hosting;

public class AppHostInfo(Assembly assembly) {

	public Assembly Assembly { get; } = assembly;

	public List<IAppModule> Modules { get; } = [];

	public List<IAppModuleOptions> Options { get; } = [];

	public List<IAppModuleRoutes> Routes { get; } = [];

	public List<IAppModulePermissions> Permissions { get; } = [];

	public AppModulesLayersInfo Layers { get; } = new();

	public AppModulesAssembliesInfo Assemblies { get; } = new();

	public class AppModulesLayersInfo {

		public List<IAppModuleLayer> Archend { get; } = [];

		public List<IAppModuleLayer> Current { get; } = [];

	}

	public class AppModulesAssembliesInfo {

		public List<Assembly> Archend { get; } = [];

		public List<Assembly> Current { get; } = [];

	}

}
