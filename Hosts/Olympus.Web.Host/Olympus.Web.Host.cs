namespace Olympus.Web.Host;

public sealed class WebHost {

	public static Task Main(string[] args) {

		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		var info = new AppHostInfo(typeof(WebHost).Assembly);

		builder.AddSettings();

		builder.AddModules(info);

		builder.AddServices();

		builder.AddRootComponents();

		return builder.BuildAndRunAsync();

	}

}
