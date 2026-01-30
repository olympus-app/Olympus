namespace Olympus.Core.Identity;

public readonly record struct PermissionInfo {

	public int Value { get; init; }

	public string Name { get; init; }

	public string Description { get; init; }

	public string Module { get; init; }

	public string Feature { get; init; }

	public string Action { get; init; }

}
