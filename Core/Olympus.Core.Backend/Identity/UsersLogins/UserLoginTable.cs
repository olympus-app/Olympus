namespace Olympus.Core.Backend.Identity;

public class UserLoginTable : EntityTable<UserLogin> {

	public const string TableName = "UsersLogins";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<UserLogin> builder) {

		builder.ToTable(TableName, SchemaName);

		builder.HasKey(ulogin => new { ulogin.LoginProvider, ulogin.ProviderKey });
		builder.Property(ulogin => ulogin.LoginProvider).HasMaxLength(64);
		builder.Property(ulogin => ulogin.ProviderKey).HasMaxLength(128);
		builder.Property(ulogin => ulogin.ProviderDisplayName).HasMaxLength(64);

		builder.HasOne<User>().WithMany().HasForeignKey(ulogin => ulogin.UserId).OnDelete(DeleteBehavior.Cascade);

	}

}
