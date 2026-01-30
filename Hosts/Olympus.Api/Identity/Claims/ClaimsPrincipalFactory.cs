using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Olympus.Api.Identity;

public class ClaimsPrincipalFactory(IDatabaseService database, UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> identityOptions) : UserClaimsPrincipalFactory<User, Role>(userManager, roleManager, identityOptions) {

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
			.Include(user => user.Claims)
			.Include(user => user.Roles).ThenInclude(urole => urole.Role).ThenInclude(role => role.Claims)
			.Include(user => user.Permissions).ThenInclude(uperm => uperm.Permission)
			.Include(user => user.Roles).ThenInclude(urole => urole.Role).ThenInclude(role => role.Permissions).ThenInclude(rperm => rperm.Permission)
			.Include(user => user.Photo).ThenInclude(photo => photo!.File)
			.AsSplitQuery().FirstOrDefaultAsync(user => user.Id == id);

	}

	private ClaimsIdentity GetUserIdentity(User user) {

		var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme, Options.ClaimsIdentity.UserNameClaimType, Options.ClaimsIdentity.RoleClaimType);

		identity.AddClaim(AppClaimsTypes.Id, user.Id);
		identity.AddClaim(AppClaimsTypes.Name, user.Name);
		identity.AddClaim(AppClaimsTypes.UserName, user.UserName);
		identity.AddClaim(AppClaimsTypes.Email, user.Email);
		identity.AddClaim(AppClaimsTypes.Title, user.Title);
		identity.AddClaim(AppClaimsTypes.Photo, user.Photo?.GetPhotoUrl());
		identity.AddClaim(AppClaimsTypes.SecurityStamp, user.SecurityStamp);

		foreach (var uclaim in user.Claims) identity.AddClaim(uclaim.ClaimType, uclaim.ClaimValue);
		foreach (var urole in user.Roles) foreach (var rclaim in urole.Role.Claims) identity.AddClaim(rclaim.ClaimType, rclaim.ClaimValue);
		foreach (var urole in user.Roles) identity.AddClaim(AppClaimsTypes.Role, urole.Role.Name);

		var rolePermissions = user.Roles.Select(urole => urole.Role).Where(role => role is not null).SelectMany(role => role.Permissions).Select(rperm => rperm.Permission.Value);
		var userPermissions = user.Permissions.Select(uperm => uperm.Permission.Value);
		var allPermissions = rolePermissions.Concat(userPermissions).Distinct();

		var packedString = PermissionsEncoder.Encode(allPermissions);

		identity.AddClaim(AppClaimsTypes.Permissions, packedString);

		return identity;

	}

}
