namespace Olympus.Core.Backend.Identity;

public class UserPhotoTable : EntityTable<UserPhoto> {

	public const string TableName = "UsersPhotos";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<UserPhoto> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.HasOne(uphoto => uphoto.User).WithOne(user => user.Photo).HasForeignKey<UserPhoto>(uphoto => uphoto.UserId).OnDelete(DeleteBehavior.Cascade);
		builder.HasOne(uphoto => uphoto.File).WithOne().HasForeignKey<UserPhoto>(uphoto => uphoto.FileId).OnDelete(DeleteBehavior.Cascade);

	}

}
