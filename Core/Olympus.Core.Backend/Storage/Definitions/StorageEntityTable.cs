namespace Olympus.Core.Backend.Storage;

public class StorageEntityTable : EntityTable<StorageEntity> {

	public const string TableName = "Files";

	public const string SchemaName = "Storage";

	public override void Configure(EntityTypeBuilder<StorageEntity> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.Property(file => file.Name).HasMaxLength(256).IsRequired();
		builder.Property(file => file.BaseName).HasMaxLength(256).IsRequired();
		builder.Property(file => file.Extension).HasMaxLength(32).IsRequired();
		builder.Property(file => file.ContentType).HasMaxLength(128).IsRequired();
		builder.Property(file => file.Bucket).HasMaxLength(64).IsRequired();
		builder.Property(file => file.Path).HasMaxLength(512).IsRequired();
		builder.Property(file => file.Size).IsRequired();

		builder.HasDiscriminator<string>("FileType")
			.HasValue<StorageFile>("File")
			.HasValue<StorageImage>("Image");

		builder.Property("FileType").HasMaxLength(64);

		builder.HasIndex(file => file.Path).IsUnique();

	}

}
