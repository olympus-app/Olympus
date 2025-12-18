namespace Olympus.Core.Archend.Storage;

public interface IFileUploadRequest : IEntityRequest {

	public Guid Id { get; init; }

}
