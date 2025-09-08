using ServiceScan.SourceGenerator;

namespace Olympus.Api.Host;

internal static partial class ModulesConfiguration {

	[GenerateServiceRegistrations(AssignableTo = typeof(IAppModuleBackendLayer), CustomHandler = nameof(AddModule), AssemblyNameFilter = "Olympus.*.Backend")]
	private static partial void AddModules(this IServiceCollection services);

	private static void AddModule<TModule>(this IServiceCollection services) where TModule : class, IAppModuleBackendLayer, new() {

		var module = new TModule();
		module.AddModule(services);

	}

	internal static void AddModules(this WebApplicationBuilder builder) {

		builder.Services.AddModules();

	}

}
