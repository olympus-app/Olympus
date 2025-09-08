namespace Olympus.Core.Backend;

public partial class CoreBackendModule : IAppModuleBackendLayer {

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityTable), Lifetime = ServiceLifetime.Transient)]
	private static partial void AddEntityTables(IServiceCollection services);

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityModel), Lifetime = ServiceLifetime.Transient)]
	private static partial void AddEntityModels(IServiceCollection services);

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityService<>), Lifetime = ServiceLifetime.Scoped)]
	private static partial void AddEntityServices(IServiceCollection services);

	public void AddModule(IServiceCollection services) {

		services.AddOptions<CoreOptions>().BindConfiguration(CoreOptions.SectionName);
		services.AddSingleton<CoreOptions>(provider => provider.GetRequiredService<IOptions<CoreOptions>>().Value);
		services.AddSingleton<IAppModuleOptions>(provider => provider.GetRequiredService<IOptions<CoreOptions>>().Value);

		AddEntityTables(services);
		AddEntityModels(services);
		AddEntityServices(services);

	}

}
