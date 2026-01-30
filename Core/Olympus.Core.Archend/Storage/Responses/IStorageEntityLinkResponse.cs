namespace Olympus.Core.Archend.Storage;

public interface IStorageEntityLinkResponse : IEntityLinkResponse {

	public string Name { get; init; }

	public string ContentType { get; init; }

	public DateTimeOffset? UpdatedAt { get; init; }

}
