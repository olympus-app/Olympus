namespace Olympus.Core.Backend;

public partial class ZeusBackend : IAppModuleBackendLayer {

	// [GenerateServiceRegistrations(AssignableTo = typeof(IEntityTable), Lifetime = ServiceLifetime.Transient)]
	// private static partial void AddEntityTables(IServiceCollection services);

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityModel), Lifetime = ServiceLifetime.Transient)]
	private static partial void AddEntityModels(IServiceCollection services);

	// [GenerateServiceRegistrations(AssignableTo = typeof(IEntityService<>), Lifetime = ServiceLifetime.Scoped)]
	// private static partial void AddEntityServices(IServiceCollection services);

	public void AddModule(IServiceCollection services) {

		services.AddOptions<ZeusOptions>().BindConfiguration(ZeusOptions.SectionName);
		services.AddSingleton<ZeusOptions>(provider => provider.GetRequiredService<IOptions<ZeusOptions>>().Value);
		services.AddSingleton<IAppModuleOptions>(provider => provider.GetRequiredService<IOptions<ZeusOptions>>().Value);

		// AddEntityTables(services);
		AddEntityModels(services);
		// AddEntityServices(services);

	}

}
