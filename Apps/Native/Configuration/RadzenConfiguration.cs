using Radzen;

namespace Olympus.Native;

internal static class RadzenConfiguration
{

	internal static void AddRadzenBlazor(this MauiAppBuilder builder)
	{

		builder.Services.AddRadzenComponents();

	}

}
