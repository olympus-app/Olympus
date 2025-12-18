namespace Olympus.Core.Backend.Storage;

public class FileEntityTable : EntityTable<FileEntity> {

	public const string TableName = "Files";

	public const string SchemaName = "Storage";

	public override void Configure(EntityTypeBuilder<FileEntity> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.Property(file => file.Name).HasMaxLength(256).IsRequired();
		builder.Property(file => file.Extension).HasMaxLength(32).IsRequired();
		builder.Property(file => file.ContentType).HasMaxLength(128).IsRequired();
		builder.Property(file => file.StorageBucket).HasMaxLength(64).IsRequired();
		builder.Property(file => file.StoragePath).HasMaxLength(512).IsRequired();
		builder.Property(file => file.ContentHash).HasMaxLength(64).IsRequired();
		builder.Property(file => file.Size).IsRequired();

		builder.HasDiscriminator<string>("FileType")
			.HasValue<ImageFile>("Image");

		builder.Property("FileType").HasMaxLength(64);

		builder.HasIndex(file => file.StoragePath).IsUnique();
		builder.HasIndex(file => file.ContentHash);

	}

}
