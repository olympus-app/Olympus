namespace Olympus.Web.Radzen;

internal static class RadzenRegistrator {

	internal static void AddRadzenServices(this WebAssemblyHostBuilder builder) {

		builder.Services.AddRadzenComponents();

	}

}
