namespace Olympus.Core.Archend.Storage;

public interface IEntityWithStorageDeleteRequest : IEntityRequest {

	public Guid Id { get; init; }

	public string ETag { get; init; }

}
