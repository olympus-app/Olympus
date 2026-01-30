using System.Reflection;

namespace Olympus.Core.Modules;

public abstract class AppModuleInfo : IAppModuleInfo {

	public abstract Assembly Assembly { get; }

	public abstract string CodeName { get; }

	public abstract string DisplayName { get; }

	public abstract string BaseRoute { get; }

	public virtual int[] ApiVersions { get; } = [1];

	public virtual List<RouteInfo> Routes { get; } = [];

	public virtual List<PermissionInfo> Permissions { get; } = [];

}
