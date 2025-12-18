using System.Security.Claims;

namespace Olympus.Core.Identity;

public sealed class AppClaimsPrincipal : ClaimsPrincipal {

	public HashSet<int> Permissions { get; }

	public AppClaimsPrincipal() => Permissions = [];

	public AppClaimsPrincipal(ClaimsIdentity identity) : base(identity) => Permissions = GetPermissions(this);

	public AppClaimsPrincipal(ClaimsPrincipal principal) : base(principal) => Permissions = GetPermissions(principal);

	private static HashSet<int> GetPermissions(ClaimsPrincipal principal) {

		var packedPermissions = principal.FindFirst(AppClaimsTypes.Permissions)?.Value;

		return string.IsNullOrEmpty(packedPermissions) ? [] : PermissionsEncoder.Decode(packedPermissions);

	}

}
