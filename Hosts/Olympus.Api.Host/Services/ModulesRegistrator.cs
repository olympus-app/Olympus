namespace Olympus.Api.Host.Services;

public static partial class ModulesRegistrator {

	[GenerateServiceRegistrations(AssignableTo = typeof(IAppModuleLayer), CustomHandler = nameof(AddModuleLayer), AssemblyNameFilter = $"{AppSettings.AppBaseName}.*")]
	private static partial void AddModulesLayers(IServiceCollection services);

	private static void AddModuleLayer<TModule>(IServiceCollection services) where TModule : class, IAppModuleLayer, new() {

		var module = new TModule();

		module.AddLayerServices(services);

	}

	[GenerateServiceRegistrations(AssignableTo = typeof(IAppModuleOptions), CustomHandler = nameof(AddModuleOptions), AssemblyNameFilter = $"{AppSettings.AppBaseName}.*")]
	private static partial void AddModulesOptions(ApiOptions options);

	private static void AddModuleOptions<TOptions>(ApiOptions options) where TOptions : class, IAppModuleOptions, new() {

		var module = new TOptions();

		options.ModulesOptions.Add(module);

	}

	public static void AddModules(this WebApplicationBuilder builder, ApiOptions options) {

		AddModulesLayers(builder.Services);

		AddModulesOptions(options);

	}

}
