namespace Olympus.Core.Settings;

public class HostSettings {

	public const string SectionName = "Host";

	public string Name { get; set; } = "Unknown";

	public string Type { get; set; } = "Unknown";

#if DEBUG
	public AppEnvironmentType Environment { get; set; } = AppEnvironmentType.Development;
#else
	public AppEnvironmentType Environment { get; set; } = AppEnvironmentType.Production;
#endif

}
