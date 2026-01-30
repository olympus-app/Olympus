namespace Olympus.Core.Backend.Storage;

public class StorageImageTable : EntityTable<StorageImage> {

	public override void Configure(EntityTypeBuilder<StorageImage> builder) {

		builder.HasBaseType<StorageEntity>();

	}

}
