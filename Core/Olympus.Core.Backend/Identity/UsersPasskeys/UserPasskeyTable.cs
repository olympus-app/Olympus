namespace Olympus.Core.Backend.Identity;

public class UserPasskeyTable : EntityTable<UserPasskey> {

	public const string TableName = "UsersPasskeys";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<UserPasskey> builder) {

		builder.ToTable(TableName, SchemaName);

		builder.HasKey(upass => upass.CredentialId);
		builder.Property(upass => upass.CredentialId).HasMaxLength(128);

		builder.HasOne<User>().WithMany().HasForeignKey(upass => upass.UserId).OnDelete(DeleteBehavior.Cascade);

	}

}
