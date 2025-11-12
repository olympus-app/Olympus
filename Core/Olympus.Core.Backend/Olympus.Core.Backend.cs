namespace Olympus.Core.Backend;

public partial class CoreModuleBackendLayer : AppModuleLayer {

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityTable), Lifetime = ServiceLifetime.Transient)]
	private static partial void AddEntityTables(IServiceCollection services);

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityModel), Lifetime = ServiceLifetime.Transient)]
	private static partial void AddEntityModels(IServiceCollection services);

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityService<>), Lifetime = ServiceLifetime.Scoped)]
	private static partial void AddEntityServices(IServiceCollection services);

	public override void AddLayerServices(IServiceCollection services) {

		AddEntityTables(services);
		AddEntityModels(services);
		AddEntityServices(services);

	}

}
