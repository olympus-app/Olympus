namespace Olympus.Core.Modules;

public interface IAppModuleOptions {

	public string Name { get; }

	public string Path { get; }

	public string ApiPath { get; }

	public string WebPath { get; }

	public int[] ApiVersions { get; }

	public AppModuleType Type { get; }

	public AppModuleCategory Category { get; }

	public Type Routes { get; }

	public Type Permissions { get; }

}
