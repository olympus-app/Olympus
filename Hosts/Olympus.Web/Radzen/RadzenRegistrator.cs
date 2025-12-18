namespace Olympus.Web.Radzen;

public static class RadzenRegistrator {

	public static void AddRadzenServices(this WebAssemblyHostBuilder builder) {

		builder.Services.AddRadzenComponents();

	}

}
