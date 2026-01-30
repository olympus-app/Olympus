using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Olympus.Web.Routing;

public partial class AppRouter : ComponentBase {

	[Inject]
	protected AppHostInfo HostInfo { get; set; } = default!;

	private static bool AllowAnonymous(Type pageType) {

		var allowAnonymousAttribute = pageType.GetCustomAttribute<AllowAnonymousAttribute>();

		return allowAnonymousAttribute is not null;

	}

	private static Type GetLayout(Type pageType) {

		var layoutAttribute = pageType.GetCustomAttribute<LayoutAttribute>();

		if (layoutAttribute is not null) return layoutAttribute.LayoutType;

		if (AllowAnonymous(pageType)) return typeof(LocksScreenLayout);

		return typeof(MainLayout);

	}

}
