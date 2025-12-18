namespace Olympus.Core.Modules;

public abstract class AppModuleOptions : ModuleSettings, IAppModuleOptions {

	public abstract string Name { get; }

	public abstract string Path { get; }

	public abstract string ApiPath { get; }

	public abstract string WebPath { get; }

	public virtual int[] ApiVersions { get; } = [1];

	public abstract AppModuleType Type { get; }

	public abstract AppModuleCategory Category { get; }

	public abstract Type Routes { get; }

	public abstract Type Permissions { get; }

}
