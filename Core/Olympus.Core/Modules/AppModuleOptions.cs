namespace Olympus.Core.Modules;

public abstract class AppModuleOptions : IAppModuleOptions {

	public abstract string CodeName { get; }

	public abstract string DisplayName { get; }

	public abstract string BaseRoute { get; }

	public abstract void Configure(IConfiguration configuration);

}
