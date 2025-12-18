namespace Olympus.Core.Archend.Endpoints;

#pragma warning disable OL0007
public class ErrorResult : IResponse {

	[JsonPropertyOrder(0)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Origin { get; set; }

	[JsonPropertyOrder(1)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Message { get; set; }

}

#pragma warning restore OL0007
