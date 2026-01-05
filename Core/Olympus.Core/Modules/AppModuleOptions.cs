namespace Olympus.Core.Modules;

public abstract class AppModuleOptions(AppModuleType type, AppModuleCategory category) : AppModule(type, category), IAppModuleOptions {

	public abstract void Configure(IConfiguration configuration);

	public static void AddOption<TOption>(IServiceCollection services) where TOption : class, IAppModuleOptions {

		services.AddOptions<TOption>().Configure<IConfiguration>(static (settings, configuration) => settings.Configure(configuration));

		services.AddSingleton<IAppModuleOptions>(static services => services.GetRequiredService<IOptions<TOption>>().Value);

		services.AddSingleton(static services => services.GetRequiredService<IOptions<TOption>>().Value);

	}

}
