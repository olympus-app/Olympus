namespace Olympus.Core.Backend.Identity;

public class UserRoleTable : EntityTable<UserRole> {

	public const string TableName = "UsersRoles";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<UserRole> builder) {

		builder.ToTable(TableName, SchemaName);

		builder.HasKey(urole => new { urole.UserId, urole.RoleId });
		builder.HasIndex(urole => new { urole.RoleId, urole.UserId }).IsUnique();

		builder.HasOne(urole => urole.User).WithMany(user => user.UserRoles).HasForeignKey(urole => urole.UserId).OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(urole => urole.Role).WithMany(role => role.RoleUsers).HasForeignKey(urole => urole.RoleId).OnDelete(DeleteBehavior.Cascade);

	}

}
