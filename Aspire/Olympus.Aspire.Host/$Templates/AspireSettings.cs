namespace Olympus.Aspire.Host;

public static class AspireSettings {

	public static class App {

		public const string ServiceName = "API";

		public const string ServiceProfile = "API";

		public const string IconName = "WindowApps";

		public const int Port = 3333;

		public static class Endpoint {

			public const string Name = "https";

			public const string DisplayText = "Home";

			public const UrlDisplayLocation DisplayLocation = UrlDisplayLocation.SummaryAndDetails;

			public const int DisplayOrder = 3;

		}

		public static class Docs {

			public const string Name = "docs";

			public const string Path = AppRoutes.ApiDocs;

			public const string DisplayText = "Docs";

			public const UrlDisplayLocation DisplayLocation = UrlDisplayLocation.SummaryAndDetails;

			public const int DisplayOrder = 2;

		}

		public static class Routes {

			public const string Name = "routes";

			public const string Path = AppRoutes.ApiRoutes;

			public const string DisplayText = "Routes";

			public const UrlDisplayLocation DisplayLocation = UrlDisplayLocation.SummaryAndDetails;

			public const int DisplayOrder = 1;

		}

	}

	public static class Database {

		public const string Name = DatabaseSettings.DatabaseName;

		public const string ImageTag = "17";

		public const string IconName = "Database";

		public const string ServiceIconName = "DatabaseMultiple";

		public const string ServiceName = DatabaseSettings.ServiceName;

		public const string ContainerName = "Postgres";

		public const ContainerLifetime Lifetime = ContainerLifetime.Persistent;

		public const string UsernameKey = "Postgres-Username";

		public const string PasswordKey = "Postgres-Password";

		public const string VolumeName = "postgres";

		public const int Port = 5432;

	}

	public static class Storage {

		public const string ImageTag = "RELEASE.2025-09-07T16-13-09Z";

		public const string IconName = "Folder";

		public const string ServiceName = StorageSettings.ServiceName;

		public const string ContainerName = "Minio";

		public const ContainerLifetime Lifetime = ContainerLifetime.Persistent;

		public const string UsernameKey = "Minio-Username";

		public const string PasswordKey = "Minio-Password";

		public const string VolumeName = "minio";

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

		public const string IconName = "Notebook";

		public const string ServiceName = CacheSettings.ServiceName;

		public const string ContainerName = "Redis";

		public const ContainerLifetime Lifetime = ContainerLifetime.Persistent;

		public const string PasswordKey = "Redis-Password";

		public const string VolumeName = "redis";

		public const int Port = 6379;

	}

}
