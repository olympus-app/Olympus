namespace Olympus.Web.Host;

public sealed class WebHost {

	public static async Task Main(string[] args) {

		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.AddSettings();

		builder.AddModules();

		builder.AddServices();

		builder.AddRootComponents();

		await builder.BuildAndRunAsync();

	}

}
