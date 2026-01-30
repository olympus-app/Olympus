namespace Olympus.Core.Archend;

public sealed partial class CoreOptions {

	partial void OnConfigure(IConfiguration configuration, ref bool handled) {

		configuration.Bind($"Modules:{CodeName}", this);

		handled = true;

	}

}
