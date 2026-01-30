namespace Olympus.Web.Host;

public static class WebHost {

	public static Task Main(string[] args) {

		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		var info = builder.GetWebHostInfo();

		builder.LoadSettings();

		builder.AddDiscoveredServices();

		builder.AddWebServices();

		return builder.BuildAndRunAsync();

	}

}
