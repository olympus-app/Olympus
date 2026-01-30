using Microsoft.AspNetCore.Authorization;

namespace Olympus.Core.Backend.Identity;

public class PermissionsRequirementsHandler : AuthorizationHandler<PermissionsRequirements> {

	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirements requirement) {

		if (!context.User.CheckPermissions(requirement.One, requirement.Any, requirement.All)) return Task.CompletedTask;

		context.Succeed(requirement);

		return Task.CompletedTask;

	}

}
