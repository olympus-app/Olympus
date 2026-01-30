namespace Olympus.Core.Modules;

public interface IAppModuleOptions : IOptionsService<IAppModuleOptions> {

	public string CodeName { get; }

	public string DisplayName { get; }

	public string BaseRoute { get; }

}
