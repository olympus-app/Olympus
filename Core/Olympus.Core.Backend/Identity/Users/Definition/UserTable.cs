using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class UserTable(AppSettings settings) : EntityTable<User> {

	public const string TableName = "Users";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<User> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.Property(user => user.Name).HasMaxLength(64).IsRequired();
		builder.Property(user => user.UserName).HasMaxLength(128);
		builder.Property(user => user.NormalizedUserName).HasMaxLength(128);
		builder.Property(user => user.Email).HasMaxLength(128);
		builder.Property(user => user.NormalizedEmail).HasMaxLength(128);
		builder.Property(user => user.Title).HasMaxLength(64);
		builder.Property(user => user.PasswordHash).HasMaxLength(128);
		builder.Property(user => user.SecurityStamp).HasMaxLength(128);
		builder.Property(user => user.ConcurrencyStamp).HasMaxLength(128);
		builder.Property(user => user.PhoneNumber).HasMaxLength(64);

		builder.HasIndex(entity => entity.UserName).IsUniqueWhenNotSoftDeleted();
		builder.HasIndex(entity => entity.NormalizedUserName).IsUniqueWhenNotSoftDeleted();
		builder.HasIndex(entity => entity.Email).IsUniqueWhenNotSoftDeleted();
		builder.HasIndex(entity => entity.NormalizedEmail).IsUniqueWhenNotSoftDeleted();

		var seed = GetSeed(settings.Admin);
		builder.HasData(seed);

	}

	public static List<User> GetSeed(AdminSettings admin) {

		return typeof(AppUsers).GetProperties(BindingFlags.Public | BindingFlags.Static)
			.Where(info => info.PropertyType == typeof(AppUser))
			.Select(info => (AppUser)info.GetValue(null)!)
			.Select(user => user.Id == admin.Id ? AdminUserSeed(admin) : AppUserSeed(user))
			.ToList();

	}

	private static User AppUserSeed(AppUser user) {

		var seed = new User {
			Id = user.Id,
			Name = user.Name,
			UserName = user.UserName,
			NormalizedUserName = user.UserName?.ToUpper(),
			Email = user.Email,
			Title = user.Title,
			NormalizedEmail = user.Email?.ToUpper(),
			SecurityStamp = Guid.Empty.ToString(),
			EmailConfirmed = false,
			CreatedById = user.Id,
			UpdatedById = user.Id,
		};

		return PrepareSeed(seed, false, true, true, true);

	}

	private static User AdminUserSeed(AdminSettings admin) {

		var seed = new User {
			Id = admin.Id,
			Name = admin.Name,
			UserName = admin.UserName,
			NormalizedUserName = admin.UserName?.ToUpper(),
			Email = admin.Email,
			Title = admin.Title,
			NormalizedEmail = admin.Email?.ToUpper(),
			SecurityStamp = Guid.Empty.ToString(),
			EmailConfirmed = true,
			CreatedById = admin.Id,
			UpdatedById = admin.Id,
		};

		var passwordHasher = new PasswordHasher<User>();
		seed.PasswordHash = passwordHasher.HashPassword(seed, admin.Password);

		return PrepareSeed(seed, true, false, false, true);

	}

}
