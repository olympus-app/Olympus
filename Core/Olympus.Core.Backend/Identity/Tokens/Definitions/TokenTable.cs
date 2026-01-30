namespace Olympus.Core.Backend.Identity;

public class TokenTable : EntityTable<Token> {

	public const string TableName = "Tokens";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<Token> builder) {

		builder.ToTable(TableName, SchemaName);

		builder.Property(token => token.Name).HasMaxLength(64);
		builder.Property(token => token.Hash).HasMaxLength(128);

		builder.HasIndex(token => token.Hash).IsUnique();
		builder.HasIndex(token => token.UserId);

	}

}
