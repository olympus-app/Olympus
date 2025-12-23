namespace Olympus.Web.Host;

public sealed class WebHost {

	public static Task Main(string[] args) {

		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		var modules = new AppModulesInfo();

		builder.AddSettings();

		builder.AddModules(modules);

		builder.AddServices();

		builder.AddRootComponents();

		return builder.BuildAndRunAsync();

	}

}
