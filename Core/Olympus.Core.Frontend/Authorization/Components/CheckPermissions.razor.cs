using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Olympus.Core.Frontend.Authorization;

public partial class CheckPermissions : ComponentBase {

	[Parameter]
	public int? One { get; set; }

	[Parameter]
	public int[]? Any { get; set; }

	[Parameter]
	public int[]? All { get; set; }

	[Parameter]
	public RenderFragment? Authorized { get; set; }

	[Parameter]
	public RenderFragment? NotAuthorized { get; set; }

	[CascadingParameter]
	private Task<AuthenticationState>? AuthStateTask { get; set; }

	private bool Allowed;

	protected override async Task OnParametersSetAsync() {

		Allowed = false;

		if (AuthStateTask is null) return;

		var authState = await AuthStateTask;

		var user = authState.User;

		if (!user.IsAuthenticated) return;

		Allowed = user.CheckPermissions(One, Any, All);

	}

}
