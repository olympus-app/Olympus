using Microsoft.AspNetCore.Authorization;

namespace Olympus.Core.Backend.Identity;

public class PermissionsRequirements(int? one = null, int[]? any = null, int[]? all = null) : IAuthorizationRequirement {

	public int? One { get; } = one;

	public int[] Any { get; } = any ?? [];

	public int[] All { get; } = all ?? [];

}
