namespace Olympus.Core.Archend;

public class CoreOptions() : AppModuleOptions(AppModuleType.Core, AppModuleCategory.Infrastructure) {

	public override void Configure(IConfiguration configuration) {

		configuration.Bind($"Modules:{ModuleName}", this);

	}

}
