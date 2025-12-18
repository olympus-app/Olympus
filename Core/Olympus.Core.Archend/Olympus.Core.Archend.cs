namespace Olympus.Core.Archend;

public class CoreModuleArchendLayer : AppModuleArchendLayer {

	public override void AddLayerServices(IServiceCollection services) {

		services.AddOptions<CoreOptions>().BindConfiguration(CoreModule.SettingsPath);
		services.AddSingleton<IAppModuleOptions>(static provider => provider.GetRequiredService<IOptions<CoreOptions>>().Value);
		services.AddSingleton<CoreOptions>(static provider => provider.GetRequiredService<IOptions<CoreOptions>>().Value);

	}

}
