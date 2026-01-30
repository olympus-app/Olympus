using System.Reflection;

namespace Olympus.Core.Hosting;

public abstract class AppHostInfo {

	public abstract Assembly Assembly { get; }

	public abstract List<Assembly> ModulesAssemblies { get; }

	public abstract List<IAppModuleInfo> ModulesInfo { get; }

}
