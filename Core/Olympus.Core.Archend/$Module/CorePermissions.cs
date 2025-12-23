namespace Olympus.Core.Archend;

public class CorePermissions() : AppModulePermissions(AppModuleType.Core, AppModuleCategory.Infrastructure) {

	public const int Base = PermissionsScheme.ModuleOffset * CoreModule.Id;

	public static class Users {

		public const int Base = CorePermissions.Base + PermissionsScheme.FeatureOffset * 1;

		public const int Read = Base + 1;

		public const int Write = Base + 2;

		public const int Delete = Base + 3;

	}

	public static class Roles {

		public const int Base = CorePermissions.Base + PermissionsScheme.FeatureOffset * 2;

		public const int Read = Base + 1;

	}

	public static class Permissions {

		public const int Base = CorePermissions.Base + PermissionsScheme.FeatureOffset * 3;

		public const int Read = Base + 1;

	}

}
