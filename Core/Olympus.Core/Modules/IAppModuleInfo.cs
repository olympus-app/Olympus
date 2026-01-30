using System.Reflection;

namespace Olympus.Core.Modules;

public interface IAppModuleInfo {

	public Assembly Assembly { get; }

	public string CodeName { get; }

	public string DisplayName { get; }

	public string BaseRoute { get; }

	public int[] ApiVersions { get; }

	public List<RouteInfo> Routes { get; }

	public List<PermissionInfo> Permissions { get; }

}
