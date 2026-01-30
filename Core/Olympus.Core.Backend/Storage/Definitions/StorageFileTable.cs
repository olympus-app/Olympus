namespace Olympus.Core.Backend.Storage;

public class StorageFileTable : EntityTable<StorageFile> {

	public override void Configure(EntityTypeBuilder<StorageFile> builder) {

		builder.HasBaseType<StorageEntity>();

	}

}
