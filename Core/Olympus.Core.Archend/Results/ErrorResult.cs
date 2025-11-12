using System.Text.Json.Serialization;

namespace Olympus.Core.Archend.Results;

public class ErrorResult {

	[JsonPropertyOrder(0)]
	public int Status { get; set; } = 500;

	[JsonPropertyOrder(1)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Code { get; set; }

	[JsonPropertyOrder(2)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Message { get; set; }

	[JsonPropertyOrder(3)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Details { get; set; }

	[JsonPropertyOrder(4)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Localizer { get; set; }

}
