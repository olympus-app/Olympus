namespace Olympus.Core.Backend.Identity;

public class UserTokenTable : EntityTable<UserToken> {

	public const string TableName = "UsersTokens";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<UserToken> builder) {

		builder.ToTable(TableName, SchemaName);

		builder.HasKey(utoken => new { utoken.UserId, utoken.LoginProvider, utoken.Name });
		builder.Property(utoken => utoken.Name).HasMaxLength(64);
		builder.Property(utoken => utoken.LoginProvider).HasMaxLength(64);

		builder.HasOne<User>().WithMany().HasForeignKey(utoken => utoken.UserId).OnDelete(DeleteBehavior.Cascade);

	}

}
