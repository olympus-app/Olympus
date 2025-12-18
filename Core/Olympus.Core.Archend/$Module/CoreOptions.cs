namespace Olympus.Core.Archend;

public class CoreOptions : AppModuleOptions {

	public override string Name { get; } = CoreModule.CodeName;

	public override string Path { get; } = CoreModule.RoutesPath;

	public override string ApiPath { get; } = Core.Web.Routes.Api + CoreModule.RoutesPath;

	public override string WebPath { get; } = Core.Web.Routes.Web + CoreModule.RoutesPath;

	public override AppModuleType Type { get; } = CoreModule.ModuleType;

	public override AppModuleCategory Category { get; } = CoreModule.ModuleCategory;

	public override Type Routes => typeof(CoreRoutes);

	public override Type Permissions => typeof(CorePermissions);

}
