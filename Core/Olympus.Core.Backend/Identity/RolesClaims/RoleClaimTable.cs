namespace Olympus.Core.Backend.Identity;

public class RoleClaimTable : EntityTable<RoleClaim> {

	public const string TableName = "RolesClaims";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<RoleClaim> builder) {

		builder.ToTable(TableName, SchemaName);

		builder.HasKey(rclaim => rclaim.Id);
		builder.Property(rclaim => rclaim.ClaimType).HasMaxLength(32);
		builder.Property(rclaim => rclaim.ClaimValue).HasMaxLength(64);

		builder.HasOne(rclaim => rclaim.Role).WithMany(role => role.RoleClaims).HasForeignKey(rclaim => rclaim.RoleId).OnDelete(DeleteBehavior.Cascade);

	}

}
