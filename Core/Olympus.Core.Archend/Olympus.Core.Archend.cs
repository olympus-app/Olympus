namespace Olympus.Core.Archend;

public class CoreModuleOptions : AppModuleOptions {

	private const AppModule Module = AppModule.Core;

	public static string SectionName => $"Modules:{Module}";

	public override string Name { get; set; } = Module.ToString();

	public override string Path { get; set; } = $"/{Module.ToString().ToLower()}";

}

public partial class CoreModuleArchendLayer : AppModuleLayer {

	public override void AddLayerServices(IServiceCollection services) {

		services.AddOptions<CoreModuleOptions>().BindConfiguration(CoreModuleOptions.SectionName);
		services.AddSingleton<IAppModuleOptions>(provider => provider.GetRequiredService<IOptions<CoreModuleOptions>>().Value);

	}

}
