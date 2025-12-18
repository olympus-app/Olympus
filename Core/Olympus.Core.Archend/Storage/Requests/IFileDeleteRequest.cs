namespace Olympus.Core.Archend.Storage;

public interface IFileDeleteRequest : IEntityRequest {

	public Guid Id { get; init; }

}
