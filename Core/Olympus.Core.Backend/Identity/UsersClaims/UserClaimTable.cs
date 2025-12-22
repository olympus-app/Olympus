namespace Olympus.Core.Backend.Identity;

public class UserClaimTable : EntityTable<UserClaim> {

	public const string TableName = "UsersClaims";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<UserClaim> builder) {

		builder.ToTable(TableName, SchemaName);

		builder.HasKey(uclaim => uclaim.Id);
		builder.Property(uclaim => uclaim.ClaimType).HasMaxLength(32);
		builder.Property(uclaim => uclaim.ClaimValue).HasMaxLength(64);

		builder.HasOne(uclaim => uclaim.User).WithMany(user => user.Claims).HasForeignKey(uclaim => uclaim.UserId).OnDelete(DeleteBehavior.Cascade);

	}

}
