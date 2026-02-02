namespace Olympus.Core.Archend.Storage;

public interface IStorageEntityDeleteRequest : IEntityRequest {

	public Guid Id { get; init; }

	public string ETag { get; init; }

}
