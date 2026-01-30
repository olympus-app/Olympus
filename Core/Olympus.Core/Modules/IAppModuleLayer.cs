namespace Olympus.Core.Modules;

public interface IAppModuleLayer {

	public string CodeName { get; }

	public string DisplayName { get; }

	public AppModuleLayerType LayerType { get; }

}
