namespace Olympus.Core.Backend.Identity;

public class UserRoleTable : EntityTable<UserRole> {

	public const string TableName = "UsersRoles";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<UserRole> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.HasKey(urole => new { urole.UserId, urole.RoleId });
		builder.HasIndex(urole => new { urole.RoleId, urole.UserId }).IsUnique();

		builder.HasOne(urole => urole.User).WithMany(user => user.Roles).HasForeignKey(urole => urole.UserId).OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(urole => urole.Role).WithMany(role => role.Users).HasForeignKey(urole => urole.RoleId).OnDelete(DeleteBehavior.Cascade);

		var seed = GetSeed();
		builder.HasData(seed);

	}

	public static List<UserRole> GetSeed() {

		return [
			PrepareSeed(
				new UserRole() {
					Id = Guid.Combine(AppUsers.Admin.Id, AppRoles.Administrators.Id),
					UserId = AppUsers.Admin.Id,
					RoleId = AppRoles.Administrators.Id,
				}, true, false, true, true
			),
		];

	}

}
