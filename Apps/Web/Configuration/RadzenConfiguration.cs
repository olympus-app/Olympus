using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

namespace Olympus.Web;

internal static class RadzenConfiguration
{

	internal static void AddRadzenBlazor(this WebAssemblyHostBuilder builder)
	{

		builder.Services.AddRadzenComponents();

	}

}
