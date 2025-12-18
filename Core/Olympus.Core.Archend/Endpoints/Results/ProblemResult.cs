namespace Olympus.Core.Archend.Endpoints;

#pragma warning disable OL0007
public class ProblemResult : IResponse {

	[JsonPropertyOrder(0)]
	public int Status { get; set; } = 500;

	[JsonPropertyOrder(2)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Message { get; set; } = "Internal server error.";

	[JsonPropertyOrder(3)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Details { get; set; }

	[JsonPropertyOrder(4)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Debug { get; set; }

	[JsonPropertyOrder(5)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public IEnumerable<ErrorResult>? Errors { get; set; }

}

#pragma warning restore OL0007
