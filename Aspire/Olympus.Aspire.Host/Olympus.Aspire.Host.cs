using static Olympus.Aspire.Host.AspireSettings;

namespace Olympus.Aspire.Host;

public static class AspireHost {

	public static void Main(string[] args) {

		var builder = DistributedApplication.CreateBuilder(args);

		builder.Configuration.LoadSettings(typeof(AspireHost).Assembly);

		var production = builder.AddDockerComposeEnvironment("Production");

		var databaseUsername = builder.AddParameter(Database.UsernameKey, true);
		var databasePassword = builder.AddParameter(Database.PasswordKey, true);
		var storageUsername = builder.AddParameter(Storage.UsernameKey, true);
		var storagePassword = builder.AddParameter(Storage.PasswordKey, true);
		var cachePassword = builder.AddParameter(Cache.PasswordKey, true);

		var postgres = builder.AddPostgres(Database.ServiceName, databaseUsername, databasePassword, Database.Port)
			.WithContainerName(Database.ContainerName.ToLower())
			.WithVolume(Database.VolumeName.ToLower(), Database.VolumeTarget.ToLower())
			.WithLifetime(Database.Lifetime)
			.WithImageTag(Database.ImageTag)
			.WithIconName(Database.ServiceIconName)
			.WithComputeEnvironment(production)
			.WithEndpointProxySupport(false)
			.AddDatabase(Database.Name)
			.WithIconName(Database.IconName);

		var minio = builder.AddMinioContainer(Storage.ServiceName, storageUsername, storagePassword, Storage.Port)
			.WithContainerName(Storage.ContainerName.ToLower())
			.WithVolume(Storage.VolumeName.ToLower(), Storage.VolumeTarget.ToLower())
			.WithLifetime(Storage.Lifetime)
			.WithImageTag(Storage.ImageTag)
			.WithIconName(Storage.IconName)
			.WithUrlForEndpoint(Storage.Service.Name, static url => {
				url.DisplayText = Storage.Service.DisplayText;
				url.DisplayOrder = Storage.Service.DisplayOrder;
				url.DisplayLocation = Storage.Service.DisplayLocation;
			})
			.WithUrlForEndpoint(Storage.Console.Name, static url => {
				url.DisplayText = Storage.Console.DisplayText;
				url.DisplayOrder = Storage.Console.DisplayOrder;
				url.DisplayLocation = Storage.Console.DisplayLocation;
			})
			.WithComputeEnvironment(production)
			.WithEndpointProxySupport(false);

		var redis = builder.AddRedis(Cache.ServiceName, Cache.Port, cachePassword)
			.WithContainerName(Cache.ContainerName.ToLower())
			.WithVolume(Cache.VolumeName.ToLower(), Cache.VolumeTarget.ToLower())
			.WithLifetime(Cache.Lifetime)
			.WithImageTag(Cache.ImageTag)
			.WithIconName(Cache.IconName)
			.WithComputeEnvironment(production)
			.WithEndpointProxySupport(false);

		var api = builder.AddProject<Projects.Olympus_Api_Host>(App.ServiceName, App.ServiceProfile)
			.WithIconName(App.IconName)
			.WithReference(postgres).WaitFor(postgres)
			.WithReference(minio).WaitFor(minio)
			.WithReference(redis).WaitFor(redis)
			.WithComputeEnvironment(production)
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
			})
			.WithExternalHttpEndpoints();

		builder.Build().Run();

	}

}
