using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Olympus.Web;

public static class WebCore {

	public static void AddServices(this WebAssemblyHostBuilder builder) {

		builder.Services.AddSingleton<IFormFactor, FormFactor>();

		builder.AddRadzenBlazor();

	}

	public static async Task RunAsync(this WebAssemblyHostBuilder builder) {

		builder.RootComponents.Add<Routes>("#app");

		builder.RootComponents.Add<HeadOutlet>("head::after");

		await builder.Build().RunAsync();

	}

}
