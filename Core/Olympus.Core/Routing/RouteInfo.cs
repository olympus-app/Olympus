namespace Olympus.Core.Routing;

public readonly record struct RouteInfo {

	public string Name { get; init; }

	public string Value { get; init; }

	public string Module { get; init; }

	public string Section { get; init; }

}
