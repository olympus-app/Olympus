using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ServiceScan.SourceGenerator;

namespace Olympus.Web.Host;

internal static partial class ModulesConfiguration {

	[GenerateServiceRegistrations(AssignableTo = typeof(IAppModuleFrontendLayer), CustomHandler = nameof(AddModule), AssemblyNameFilter = "Olympus.*.Frontend")]
	private static partial void AddModules(this IServiceCollection services);

	private static void AddModule<TModule>(this IServiceCollection services) where TModule : class, IAppModuleFrontendLayer, new() {

		var module = new TModule();
		module.AddModule(services);

	}

	internal static void AddModules(this WebAssemblyHostBuilder builder) {

		builder.Services.AddModules();

	}

}
