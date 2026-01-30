namespace Olympus.Core.Archend.Storage;

public interface IEntityWithStorageUploadRequest : IEntityRequest {

	public Guid Id { get; init; }

	public string ETag { get; init; }

}
