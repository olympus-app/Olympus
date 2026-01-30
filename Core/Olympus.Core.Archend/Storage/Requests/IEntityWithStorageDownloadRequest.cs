namespace Olympus.Core.Archend.Storage;

public interface IEntityWithStorageDownloadRequest : IEntityRequest {

	public Guid Id { get; init; }

	public string? ETag { get; init; }

}
