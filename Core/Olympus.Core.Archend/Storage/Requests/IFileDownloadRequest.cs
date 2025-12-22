namespace Olympus.Core.Archend.Storage;

public interface IFileDownloadRequest : IEntityRequest {

	public Guid Id { get; init; }

	public string? ETag { get; init; }

}
