namespace Olympus.Core.Backend.Storage;

public class ImageFileTable : EntityTable<ImageFile> {

	public override void Configure(EntityTypeBuilder<ImageFile> builder) {

		builder.HasBaseType<FileEntity>();

	}

}
