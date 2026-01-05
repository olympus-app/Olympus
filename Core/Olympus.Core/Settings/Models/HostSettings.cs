namespace Olympus.Core.Settings;

public class HostSettings : Settings {

#if DEBUG
	public AppEnvironmentType Environment { get; } = AppEnvironmentType.Development;
#else
	public AppEnvironmentType Environment { get; } = AppEnvironmentType.Production;
#endif

	public string Name { get; set; } = "Unknown";

	public string Type { get; set; } = "Unknown";

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Host", this);

	}

}
