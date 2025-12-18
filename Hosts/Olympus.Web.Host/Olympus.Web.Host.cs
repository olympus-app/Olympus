namespace Olympus.Web.Host;

public sealed class WebHost {

	public static Task Main(string[] args) {

		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.AddSettings();

		builder.AddModules();

		builder.AddServices();

		builder.AddRootComponents();

		return builder.BuildAndRunAsync();

	}

}
