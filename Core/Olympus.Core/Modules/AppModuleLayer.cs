namespace Olympus.Core.Modules;

public abstract class AppModuleLayer : IAppModuleLayer {

	public abstract void AddLayerServices(IServiceCollection services);

}
