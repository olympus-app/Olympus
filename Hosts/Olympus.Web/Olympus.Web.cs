namespace Olympus.Web;

public static class Web {

	public static void AddWebServices(this WebAssemblyHostBuilder builder) {

		ServicesRegistrator.AddWebServices(builder);

		builder.AddRadzenServices();

		builder.AddRootComponents();

	}

	public static void AddRootComponents(this WebAssemblyHostBuilder builder) {

		builder.RootComponents.Add<AppRouter>("#app");

		builder.RootComponents.Add<HeadOutlet>("head::after");

	}

	public static Task BuildAndRunAsync(this WebAssemblyHostBuilder builder) {

		var app = builder.Build();

		return app.RunAsync();

	}

}
