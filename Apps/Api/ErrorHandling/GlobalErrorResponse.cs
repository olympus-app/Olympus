using System.Text.Json.Serialization;

namespace Olympus.Api;

public class GlobalErrorResponse {

	[JsonPropertyOrder(0)]
	public int Error { get; set; } = 500;

	[JsonPropertyOrder(1)]
	public string Message { get; set; } = AppErrors.Values.UnknownError;

	[JsonPropertyOrder(2)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Details { get; set; } = null;

	[JsonPropertyOrder(3)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Localizer { get; set; } = null;

	[JsonPropertyOrder(4)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Resource { get; set; } = null;

	[JsonPropertyOrder(5)]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Identifier { get; set; } = null;

}
