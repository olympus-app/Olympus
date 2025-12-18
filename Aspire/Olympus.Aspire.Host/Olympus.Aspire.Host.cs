using static Olympus.Aspire.Host.AspireSettings;

namespace Olympus.Aspire.Host;

public static class AspireHost {

	public static void Main(string[] args) {

		var builder = DistributedApplication.CreateBuilder(args);

		builder.Configuration.LoadSettings();

		var databaseUsername = builder.AddParameter(Database.UsernameKey, true);
		var databasePassword = builder.AddParameter(Database.PasswordKey, true);
		var storageUsername = builder.AddParameter(Storage.UsernameKey, true);
		var storagePassword = builder.AddParameter(Storage.PasswordKey, true);
		var cachePassword = builder.AddParameter(Cache.PasswordKey, true);

		var database = builder.AddPostgres(Database.ServiceName, databaseUsername, databasePassword, Database.Port)
			.WithEndpointProxySupport(false)
			.WithContainerName(Database.DisplayName)
			.WithLifetime(Database.Lifetime)
			.WithImageTag(Database.ImageTag)
			.WithIconName(Database.IconName)
			.WithDataVolume(Database.VolumeName)
			.AddDatabase(Database.Name);

		var storage = builder.AddMinioContainer(Storage.ServiceName, storageUsername, storagePassword, Storage.Port)
			.WithEndpointProxySupport(false)
			.WithContainerName(Storage.DisplayName)
			.WithLifetime(Storage.Lifetime)
			.WithImageTag(Storage.ImageTag)
			.WithIconName(Storage.IconName)
			.WithDataVolume(Storage.VolumeName)
			.WithUrlForEndpoint(Storage.Service.Name, static url => {
				url.DisplayText = Storage.Service.DisplayText;
				url.DisplayOrder = Storage.Service.DisplayOrder;
				url.DisplayLocation = Storage.Service.DisplayLocation;
			})
			.WithUrlForEndpoint(Storage.Console.Name, static url => {
				url.DisplayText = Storage.Console.DisplayText;
				url.DisplayOrder = Storage.Console.DisplayOrder;
				url.DisplayLocation = Storage.Console.DisplayLocation;
			});

		var cache = builder.AddRedis(Cache.ServiceName, Cache.Port, cachePassword)
			.WithEndpointProxySupport(false)
			.WithContainerName(Cache.DisplayName)
			.WithLifetime(Cache.Lifetime)
			.WithImageTag(Cache.ImageTag)
			.WithIconName(Cache.IconName)
			.WithDataVolume(Cache.VolumeName);

		var api = builder.AddProject<Projects.Olympus_Api_Host>(App.ServiceName, App.ServiceProfile)
			.WithIconName(App.IconName)
			.WithReference(database).WaitFor(database)
			.WithReference(storage).WaitFor(storage)
			.WithReference(cache).WaitFor(cache)
			.WithUrlForEndpoint(App.Endpoint.Name, static url => {
				url.DisplayText = App.Endpoint.DisplayText;
				url.DisplayLocation = App.Endpoint.DisplayLocation;
				url.DisplayOrder = App.Endpoint.DisplayOrder;
			})
			.WithUrls(static context => {
				var endpoint = context.GetEndpoint(App.Endpoint.Name)!;
				context.Urls.Add(new ResourceUrlAnnotation() {
					Url = endpoint.Url + App.Docs.Path,
					DisplayText = App.Docs.DisplayText,
					DisplayLocation = App.Docs.DisplayLocation,
					DisplayOrder = App.Docs.DisplayOrder,
				});
				context.Urls.Add(new ResourceUrlAnnotation() {
					Url = endpoint.Url + App.Routes.Path,
					DisplayText = App.Routes.DisplayText,
					DisplayLocation = App.Routes.DisplayLocation,
					DisplayOrder = App.Routes.DisplayOrder,
				});
			});

		builder.Build().Run();

	}

}
