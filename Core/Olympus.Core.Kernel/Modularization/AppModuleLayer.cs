namespace Olympus.Core.Kernel.Modularization;

public abstract class AppModuleLayer : IAppModuleLayer {

	public abstract void AddLayerServices(IServiceCollection services);

}
