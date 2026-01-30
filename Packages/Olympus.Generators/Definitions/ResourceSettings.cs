namespace Olympus.Generators;

public readonly record struct ResourceSettings {

	public required string? RootNamespace { get; init; }

	public required string? Namespace { get; init; }

	public required bool IsPublic { get; init; }

	public required string? ClassName { get; init; }

	public required string? DefaultLang { get; init; }

	public required bool EmitFormatMethods { get; init; }

}
