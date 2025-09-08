namespace Olympus.Aether.Backend;

public partial class AetherBackend : IBackendModule
{

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityTable), Lifetime = ServiceLifetime.Transient)]
	private static partial void AddEntityTables(IServiceCollection services);

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityModel), Lifetime = ServiceLifetime.Transient)]
	private static partial void AddEntityModels(IServiceCollection services);

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityService<>), Lifetime = ServiceLifetime.Scoped)]
	private static partial void AddEntityServices(IServiceCollection services);

	public void AddModule(IServiceCollection services)
	{

		AddEntityTables(services);
		AddEntityModels(services);
		AddEntityServices(services);

	}

}
