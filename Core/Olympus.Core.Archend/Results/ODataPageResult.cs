using System.Text.Json.Serialization;

namespace Olympus.Core.Archend.Results;

public class ODataPageNoMetadataResult<T> {

	[JsonPropertyOrder(0)]
	[JsonPropertyName("value")]
	public required IEnumerable<T> Value { get; set; }

}

public class ODataPageMinimalMetadataResult<T> {

	[JsonPropertyOrder(0)]
	[JsonPropertyName("@odata.context")]
	public required string ODataContext { get; set; }

	[JsonPropertyOrder(1)]
	[JsonPropertyName("@odata.count")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public long? ODataCount { get; set; }

	[JsonPropertyOrder(2)]
	[JsonPropertyName("value")]
	public required IEnumerable<T> Value { get; set; }

	[JsonPropertyOrder(3)]
	[JsonPropertyName("@odata.nextLink")]
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? ODataNextLink { get; set; }

}
