namespace Olympus.Core.Archend;

public static class CoreRoutes {

	public const string Base = CoreModule.RoutesPath;

	public static class Users {

		public const string Base = CoreRoutes.Base + "/users";

		public const string Query = Base + Routes.Query;

		public const string Create = Base + Routes.Create;

		public const string Read = Base + Routes.Read;

		public const string Update = Base + Routes.Update;

		public const string Delete = Base + Routes.Delete;

		public const string Photo = Base + "/{id}/photo";

	}

	public static class Roles {

		public const string Base = CoreRoutes.Base + "/roles";

		public const string Query = Base + Routes.Query;

		public const string Read = Base + Routes.Read;

	}

	public static class Permissions {

		public const string Base = CoreRoutes.Base + "/permissions";

		public const string List = Base + Routes.Query;

	}

}
