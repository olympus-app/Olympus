namespace Olympus.Generators;

public readonly record struct ResourceInformation {

	public required CompilationInformation CompilationInformation { get; init; }

	public required ResourceSettings ResourceSettings { get; init; }

	public required AdditionalText ResourceFile { get; init; }

	public required string ResourceFileName { get; init; }

	public required string ResourceName { get; init; }

	public required string? Namespace { get; init; }

	public required string ClassName { get; init; }

}
