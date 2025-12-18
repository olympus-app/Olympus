using static Olympus.Core.Web.Routes;

namespace Olympus.Aspire.Host;

public static class AspireSettings {

	public static class App {

		public const string ServiceName = AppSettings.ServiceName;

		public const string ServiceProfile = "API";

		public const string IconName = "Apps";

		public const int Port = 3333;

		public static class Endpoint {

			public const string Name = "https";

			public const string DisplayText = "Home";

			public const UrlDisplayLocation DisplayLocation = UrlDisplayLocation.SummaryAndDetails;

			public const int DisplayOrder = 3;

		}

		public static class Docs {

			public const string Name = "docs";

			public const string Path = $"/{ApiDocs}";

			public const string DisplayText = "Docs";

			public const UrlDisplayLocation DisplayLocation = UrlDisplayLocation.SummaryAndDetails;

			public const int DisplayOrder = 2;

		}

		public static class Routes {

			public const string Name = "routes";

			public const string Path = $"/{ApiRoutes}";

			public const string DisplayText = "Routes";

			public const UrlDisplayLocation DisplayLocation = UrlDisplayLocation.SummaryAndDetails;

			public const int DisplayOrder = 1;

		}

	}

	public static class Database {

		public const string Name = DatabaseSettings.DatabaseName;

		public const string ImageTag = "17";

		public const string IconName = "Database";

		public const string ServiceName = DatabaseSettings.ServiceName;

		public const string DisplayName = "Olympus-Database";

		public const ContainerLifetime Lifetime = ContainerLifetime.Persistent;

		public const string UsernameKey = "DatabaseUsername";

		public const string PasswordKey = "DatabasePassword";

		public const string VolumeName = "olympus_database";

		public const int Port = 5432;

	}

	public static class Storage {

		public const string ImageTag = "latest";

		public const string IconName = "DocumentFolder";

		public const string ServiceName = StorageSettings.ServiceName;

		public const string DisplayName = "Olympus-Storage";

		public const ContainerLifetime Lifetime = ContainerLifetime.Persistent;

		public const string UsernameKey = "StorageUsername";

		public const string PasswordKey = "StoragePassword";

		public const string VolumeName = "olympus_storage";

		public const int Port = 9000;

		public static class Service {

			public const string Name = "http";

			public const string DisplayText = "Service";

			public const UrlDisplayLocation DisplayLocation = UrlDisplayLocation.SummaryAndDetails;

			public const int DisplayOrder = 2;

		}

		public static class Console {

			public const string Name = "console";

			public const string DisplayText = "Console";

			public const UrlDisplayLocation DisplayLocation = UrlDisplayLocation.SummaryAndDetails;

			public const int DisplayOrder = 1;

		}

	}

	public static class Cache {

		public const string ImageTag = "8";

		public const string IconName = "BookDatabase";

		public const string ServiceName = CacheSettings.ServiceName;

		public const string DisplayName = "Olympus-Cache";

		public const ContainerLifetime Lifetime = ContainerLifetime.Persistent;

		public const string PasswordKey = "CachePassword";

		public const string VolumeName = "olympus_cache";

		public const int Port = 6379;

	}

}
