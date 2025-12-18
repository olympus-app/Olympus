namespace Olympus.Web;

public static class Web {

	public static void AddServices(this WebAssemblyHostBuilder builder) {

		builder.AddWebServices();

		builder.AddRadzenServices();

	}

	public static void AddRootComponents(this WebAssemblyHostBuilder builder) {

		builder.RootComponents.Add<Core.Frontend.Routes>("#app");

		builder.RootComponents.Add<HeadOutlet>("head::after");

	}

	public static Task BuildAndRunAsync(this WebAssemblyHostBuilder builder) {

		var app = builder.Build();

		return app.RunAsync();

	}

}
