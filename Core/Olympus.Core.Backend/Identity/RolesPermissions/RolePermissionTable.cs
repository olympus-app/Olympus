namespace Olympus.Core.Backend.Identity;

public class RolePermissionTable : EntityTable<RolePermission> {

	public const string TableName = "RolesPermissions";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<RolePermission> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.HasIndex(rperm => new { rperm.RoleId, rperm.PermissionId }).IsUnique();

		builder.HasOne(rperm => rperm.Role).WithMany(role => role.RolePermissions).HasForeignKey(rperm => rperm.RoleId).OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(rperm => rperm.Permission).WithMany(perm => perm.PermissionRoles).HasForeignKey(rperm => rperm.PermissionId).OnDelete(DeleteBehavior.Cascade);

	}

}
