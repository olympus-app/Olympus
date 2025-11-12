namespace Olympus.Web.Host.Modularization;

internal static partial class ModulesRegistrator {

	[GenerateServiceRegistrations(AssignableTo = typeof(IAppModuleLayer), CustomHandler = nameof(AddModule), AssemblyNameFilter = "*")]
	private static partial void AddModules(this IServiceCollection services);

	private static void AddModule<TModule>(this IServiceCollection services) where TModule : class, IAppModuleLayer, new() {

		var module = new TModule();
		module.AddLayerServices(services);

	}

	internal static void AddModules(this WebAssemblyHostBuilder builder) {

		builder.Services.AddModules();

	}

}
