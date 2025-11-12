using System.Text.Json.Serialization;

namespace Olympus.Core.Archend.Results;

public class ODataItemNoMetadataResult<TEntity> { }

public class ODataItemMinimalMetadataResult<TEntity> {

	[JsonPropertyOrder(0)]
	[JsonPropertyName("@odata.context")]
	public required string ODataContext { get; set; }

	[JsonPropertyOrder(1)]
	[JsonPropertyName("@odata.etag")]
	public required string ODataEtag { get; set; }

}
