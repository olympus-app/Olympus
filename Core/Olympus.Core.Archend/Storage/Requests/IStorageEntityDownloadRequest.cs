namespace Olympus.Core.Archend.Storage;

public interface IStorageEntityDownloadRequest : IEntityRequest {

	public Guid Id { get; init; }

	public string? ETag { get; init; }

}
