using System.Security.Claims;

namespace Olympus.Core.Identity;

public sealed class AppClaimsPrincipal : ClaimsPrincipal {

	public HashSet<int> Permissions { get; }

	public AppClaimsPrincipal() => Permissions = [];

	public AppClaimsPrincipal(ClaimsIdentity identity) : base(identity) => Permissions = LoadPermissions(this);

	public AppClaimsPrincipal(ClaimsPrincipal principal) : base(principal) => Permissions = LoadPermissions(principal);

	private static HashSet<int> LoadPermissions(ClaimsPrincipal principal) {

		var permissions = principal.FindFirst(AppClaimsTypes.Permissions)?.Value;

		return string.IsNullOrEmpty(permissions) ? [] : PermissionsEncoder.Decode(permissions);

	}

}
