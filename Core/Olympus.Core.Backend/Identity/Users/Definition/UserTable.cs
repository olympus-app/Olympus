using System.Reflection;

namespace Olympus.Core.Backend.Identity;

public class UserTable : EntityTable<User> {

	public const string TableName = "Users";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<User> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.Property(user => user.Name).HasMaxLength(64).IsRequired();
		builder.Property(user => user.JobTitle).HasMaxLength(64);
		builder.Property(user => user.Department).HasMaxLength(64);
		builder.Property(user => user.OfficeLocation).HasMaxLength(64);
		builder.Property(user => user.Country).HasMaxLength(64);

		builder.Property(user => user.Email).HasMaxLength(128);
		builder.Property(user => user.NormalizedEmail).HasMaxLength(128);
		builder.Property(user => user.UserName).HasMaxLength(128);
		builder.Property(user => user.NormalizedUserName).HasMaxLength(128);
		builder.Property(user => user.PasswordHash).HasMaxLength(128);
		builder.Property(user => user.SecurityStamp).HasMaxLength(128);
		builder.Property(user => user.ConcurrencyStamp).HasMaxLength(128);
		builder.Property(user => user.PhoneNumber).HasMaxLength(64);

		builder.HasIndex(entity => entity.Email).IsUniqueWhenNotSoftDeleted();
		builder.HasIndex(entity => entity.UserName).IsUniqueWhenNotSoftDeleted();
		builder.HasIndex(entity => entity.NormalizedUserName).IsUniqueWhenNotSoftDeleted();

		var seed = GetSeed();
		builder.HasData(seed);

	}

	private static IEnumerable<User> GetSeed() {

		return typeof(AppUsers).GetProperties(BindingFlags.Public | BindingFlags.Static)
			.Where(info => info.PropertyType == typeof(AppUser))
			.Select(info => (AppUser)info.GetValue(null)!)
			.Select(CreateSeed);

	}

	private static User CreateSeed(AppUser appUser) {

		return PrepareSeed(
			new User() {
				Id = appUser.Id,
				Name = appUser.Name,
				Email = appUser.Email,
				NormalizedEmail = appUser.Email?.ToUpper(),
				UserName = appUser.UserName,
				NormalizedUserName = appUser.UserName?.ToUpper(),
				SecurityStamp = Guid.Empty.ToString(),
				CreatedById = appUser.Id,
				UpdatedById = appUser.Id,
			}, false, true
		);

	}

}
