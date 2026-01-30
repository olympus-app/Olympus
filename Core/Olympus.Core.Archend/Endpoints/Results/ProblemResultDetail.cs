namespace Olympus.Core.Archend.Endpoints;

public class ProblemResultDetail : IResponse {

	[JsonPropertyOrder(0)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Origin { get; set; }

	[JsonPropertyOrder(1)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Message { get; set; }

}
