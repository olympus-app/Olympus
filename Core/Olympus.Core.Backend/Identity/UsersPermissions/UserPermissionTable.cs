namespace Olympus.Core.Backend.Identity;

public class UserPermissionTable : EntityTable<UserPermission> {

	public const string TableName = "UsersPermissions";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<UserPermission> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.HasIndex(uperm => new { uperm.UserId, uperm.PermissionId }).IsUnique();

		builder.HasOne(uperm => uperm.User).WithMany(user => user.Permissions).HasForeignKey(uperm => uperm.UserId).OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(uperm => uperm.Permission).WithMany(perm => perm.Users).HasForeignKey(uperm => uperm.PermissionId).OnDelete(DeleteBehavior.Cascade);

	}

}
