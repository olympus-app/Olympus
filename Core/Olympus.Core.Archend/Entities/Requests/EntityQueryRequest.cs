namespace Olympus.Core.Archend.Entities;

public abstract record EntityQueryRequest : IEntityQueryRequest {

	[JsonPropertyOrder(-9999)]
	[QueryParam(IsRequired = false)]
	public int? Page {
		get => field > 0 ? field : 1;
		init => field = Math.Max(value ?? 1, 1);
	}

	[JsonPropertyOrder(-9998)]
	[QueryParam(IsRequired = false)]
	public int? PageSize {
		get => field > 0 ? field : 10;
		init => field = Math.Min(value ?? 10, 100);
	}

	[JsonPropertyOrder(-9997)]
	[QueryParam(IsRequired = false)]
	public string? Filter { get; init; } = string.Empty;

	[JsonPropertyOrder(-9996)]
	[QueryParam(IsRequired = false)]
	public string? OrderBy { get; init; } = string.Empty;

}
