using Microsoft.AspNetCore.Components;

namespace Olympus.Web.Routing;

public partial class LoginRedirect : ComponentBase {

	[Inject]
	protected NavigationManager Navigation { get; set; } = default!;

	protected override void OnInitialized() {

		var returnUrl = Navigation.ToBaseRelativePath(Navigation.Uri);

		var loginUrl = AppUriBuilder.FromWeb(CoreRoutes.Identity.Login).WithQuery("returnUrl", returnUrl);

		Navigation.NavigateTo(loginUrl);

	}

}
