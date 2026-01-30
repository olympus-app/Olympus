using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Olympus.Core.Frontend.Layouts;

public sealed partial class LocksScreenLayout : LayoutComponentBase, IDisposable {

	[Inject]
	public LocalizationManager LocalizationManager { get; set; } = default!;

	protected override void OnInitialized() {

		LocalizationManager.CultureChanged += OnCultureChanged;

	}

	private void OnCultureChanged(CultureInfo culture) {

		InvokeAsync(StateHasChanged);

	}

	public void Dispose() {

		LocalizationManager.CultureChanged -= OnCultureChanged;

	}

}
