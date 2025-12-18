using System.Reflection;

namespace Olympus.Core.Backend.Identity;

public class PermissionTable(IEnumerable<IAppModuleOptions> modules) : EntityTable<Permission> {

	public const string TableName = "Permissions";

	public const string SchemaName = "Identity";

	public override void Configure(EntityTypeBuilder<Permission> builder) {

		builder.Prepare(TableName, SchemaName);

		builder.Property(permission => permission.Name).HasMaxLength(64);
		builder.Property(permission => permission.Description).HasMaxLength(128);
		builder.Property(permission => permission.Module).HasMaxLength(64);
		builder.Property(permission => permission.Feature).HasMaxLength(64);
		builder.Property(permission => permission.Action).HasMaxLength(64);

		builder.HasIndex(permission => permission.Value).IsUnique();

		var seed = GetSeed(modules);
		builder.HasData(seed);

	}

	private static List<Permission> GetSeed(IEnumerable<IAppModuleOptions> modules) {

		var permissionsToSeed = new List<Permission>();

		foreach (var module in modules) {

			if (module.Permissions is null) continue;

			var extracted = ExtractPermissions(module.Permissions, module.Name);

			permissionsToSeed.AddRange(extracted);

		}

		return permissionsToSeed;

	}

	private static List<Permission> ExtractPermissions(Type permissionsType, string moduleName) {

		var results = new List<Permission>();

		foreach (var featureType in permissionsType.GetNestedTypes(BindingFlags.Public | BindingFlags.Static)) {

			var featureName = featureType.Name;

			var actionFields = featureType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
				.Where(field => field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(int));

			foreach (var field in actionFields) {

				var actionName = field.Name;
				var fullName = $"{moduleName}.{featureName}.{actionName}";
				var value = (int)field.GetValue(null)!;

				var permission = PrepareSeed(
					new Permission() {
						Id = Guid.Parse($"00000000-0000-0000-0000-{value:X12}"),
						Value = value,
						Name = fullName,
						Description = fullName,
						Module = moduleName,
						Feature = featureName,
						Action = actionName,
					}
				);

				results.Add(permission);

			}

		}

		return results;

	}

}
