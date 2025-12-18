using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Olympus.Api.Identity;

public class ClaimsPrincipalFactory(IDatabaseContext database, UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> identityOptions) : UserClaimsPrincipalFactory<User, Role>(userManager, roleManager, identityOptions) {

	public override async Task<ClaimsPrincipal> CreateAsync(User user) {

		var identity = await GenerateClaimsAsync(user);

		return new AppClaimsPrincipal(identity);

	}

	protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user) {

		var data = await GetUserDataAsync(user.Id);
		if (data is null) return new ClaimsIdentity();

		return GetUserIdentity(data);

	}

	private Task<User?> GetUserDataAsync(Guid id) {

		return database.Set<User>().AsNoTracking()
			.Include(user => user.UserClaims)
			.Include(user => user.UserRoles).ThenInclude(urole => urole.Role).ThenInclude(role => role.RoleClaims)
			.Include(user => user.UserPermissions).ThenInclude(uperm => uperm.Permission)
			.Include(user => user.UserRoles).ThenInclude(urole => urole.Role).ThenInclude(role => role.RolePermissions).ThenInclude(rperm => rperm.Permission)
			.AsSplitQuery().FirstOrDefaultAsync(user => user.Id == id);

	}

	private ClaimsIdentity GetUserIdentity(User user) {

		var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme, Options.ClaimsIdentity.UserNameClaimType, Options.ClaimsIdentity.RoleClaimType);

		identity.AddClaim(AppClaimsTypes.Id, user.Id);
		identity.AddClaim(AppClaimsTypes.Name, user.Name);
		identity.AddClaim(AppClaimsTypes.Email, user.Email);
		identity.AddClaim(AppClaimsTypes.UserName, user.UserName);
		identity.AddClaim(AppClaimsTypes.JobTitle, user.JobTitle);
		identity.AddClaim(AppClaimsTypes.Department, user.Department);
		identity.AddClaim(AppClaimsTypes.OfficeLocation, user.OfficeLocation);
		identity.AddClaim(AppClaimsTypes.Country, user.Country);
		identity.AddClaim(AppClaimsTypes.SecurityStamp, user.SecurityStamp);

		foreach (var uclaim in user.UserClaims) identity.AddClaim(uclaim.ClaimType, uclaim.ClaimValue);
		foreach (var urole in user.UserRoles) foreach (var rclaim in urole.Role.RoleClaims) identity.AddClaim(rclaim.ClaimType, rclaim.ClaimValue);
		foreach (var urole in user.UserRoles) identity.AddClaim(AppClaimsTypes.Role, urole.Role.Name);

		var rolePermissions = user.UserRoles.Select(urole => urole.Role).Where(role => role is not null).SelectMany(role => role.RolePermissions).Select(rperm => rperm.Permission.Value);
		var userPermissions = user.UserPermissions.Select(uperm => uperm.Permission.Value);
		var allPermissions = rolePermissions.Concat(userPermissions).Distinct();

		var packedString = PermissionsEncoder.Encode(allPermissions);

		identity.AddClaim(AppClaimsTypes.Permissions, packedString);

		return identity;

	}

}
