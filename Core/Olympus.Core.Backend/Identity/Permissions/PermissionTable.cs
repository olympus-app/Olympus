namespace Olympus.Core.Backend.Identity;

public class PermissionTable(IEnumerable<IAppModuleInfo> modules) : EntityTable<Permission> {

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

	public static List<Permission> GetSeed(IEnumerable<IAppModuleInfo> modules) {

		var permissions = new List<Permission>();

		foreach (var module in modules) {

			var extracted = GetSeed(module);

			permissions.AddRange(extracted);

		}

		return permissions;

	}

	public static List<Permission> GetSeed(IAppModuleInfo module) {

		var permissions = new List<Permission>();

		foreach (var info in module.Permissions) {

			var permission = PrepareSeed(
				new Permission() {
					Id = Guid.From(info.Value),
					Value = info.Value,
					Name = info.Name,
					Description = info.Description,
					Module = info.Module,
					Feature = info.Feature,
					Action = info.Action,
				}, true, false, true, true
			);

			permissions.Add(permission);

		}

		return permissions;

	}

}
