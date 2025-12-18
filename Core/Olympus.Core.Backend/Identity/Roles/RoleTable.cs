using System.Reflection;

namespace Olympus.Core.Backend.Identity;

public class RoleTable : EntityTable<Role> {

	public const string TableName = "Roles";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<Role> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.Property(role => role.Name).HasMaxLength(64);
		builder.Property(role => role.Description).HasMaxLength(128);
		builder.Property(role => role.NormalizedName).HasMaxLength(128);
		builder.Property(role => role.ConcurrencyStamp).HasMaxLength(128);

		var seed = GetSeed();
		builder.HasData(seed);

	}

	private static IEnumerable<Role> GetSeed() {

		return typeof(AppRoles).GetProperties(BindingFlags.Public | BindingFlags.Static)
			.Where(info => info.PropertyType == typeof(AppRole))
			.Select(info => (AppRole)info.GetValue(null)!)
			.Select(CreateSeed);

	}

	private static Role CreateSeed(AppRole appRole) {

		return PrepareSeed(
			new Role() {
				Id = appRole.Id,
				Name = appRole.Name,
			}
		);

	}

}
