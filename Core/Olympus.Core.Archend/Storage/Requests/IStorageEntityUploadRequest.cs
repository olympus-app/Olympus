namespace Olympus.Core.Archend.Storage;

public interface IStorageEntityUploadRequest : IEntityRequest {

	public Guid Id { get; init; }

	public string ETag { get; init; }

}
