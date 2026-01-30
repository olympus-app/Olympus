namespace Olympus.Core.Modules;

public abstract class AppModuleLayer : IAppModuleLayer {

	public abstract string CodeName { get; }

	public abstract string DisplayName { get; }

	public abstract AppModuleLayerType LayerType { get; }

}
