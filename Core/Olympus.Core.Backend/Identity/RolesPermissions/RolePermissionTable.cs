namespace Olympus.Core.Backend.Identity;

public class RolePermissionTable(IEnumerable<IAppModulePermissions> modules) : EntityTable<RolePermission> {

	public const string TableName = "RolesPermissions";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<RolePermission> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.HasIndex(rperm => new { rperm.RoleId, rperm.PermissionId }).IsUnique();

		builder.HasOne(rperm => rperm.Role).WithMany(role => role.Permissions).HasForeignKey(rperm => rperm.RoleId).OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(rperm => rperm.Permission).WithMany(perm => perm.Roles).HasForeignKey(rperm => rperm.PermissionId).OnDelete(DeleteBehavior.Cascade);

		var seed = GetSeed(modules);
		builder.HasData(seed);

	}

	public static List<RolePermission> GetSeed(IEnumerable<IAppModulePermissions> modules) {

		var seed = new List<RolePermission>();
		var roles = RoleTable.GetSeed();
		var permissions = PermissionTable.GetSeed(modules);

		foreach (var role in roles) {

			if (role.Id != AppRoles.Administrators.Id) continue;

			foreach (var permission in permissions) {

				seed.Add(
					PrepareSeed(
						new RolePermission() {
							Id = Guid.Combine(role.Id, permission.Id),
							RoleId = role.Id,
							PermissionId = permission.Id,
						}, true, false, true, true
					)
				);

			}

		}

		return seed;

	}

}
