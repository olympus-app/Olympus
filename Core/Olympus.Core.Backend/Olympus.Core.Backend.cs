namespace Olympus.Core.Backend;

public partial class CoreModuleBackendLayer : AppModuleBackendLayer {

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityTable), Lifetime = ServiceLifetime.Transient, AsImplementedInterfaces = false, AsSelf = false)]
	private static partial void AddEntityTables(IServiceCollection services);

	[GenerateServiceRegistrations(AssignableTo = typeof(IEntityQueryMapper<>), Lifetime = ServiceLifetime.Singleton, AsImplementedInterfaces = false, AsSelf = false)]
	private static partial void AddEntityQueryMappers(IServiceCollection services);

	public override void AddLayerServices(IServiceCollection services) {

		AddEntityTables(services);
		AddEntityQueryMappers(services);

	}

}
