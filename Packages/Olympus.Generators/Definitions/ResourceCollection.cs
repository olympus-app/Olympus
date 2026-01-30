namespace Olympus.Generators;

public readonly record struct ResourceCollection {

	public required ResourceInformation BaseInformation { get; init; }

	public required ImmutableDictionary<CultureInfo, AdditionalText> OtherLanguages { get; init; }

	public required string FileHintName { get; init; }

}
