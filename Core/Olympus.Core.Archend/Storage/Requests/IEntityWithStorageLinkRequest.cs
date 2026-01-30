namespace Olympus.Core.Archend.Storage;

public interface IEntityWithStorageLinkRequest : IEntityRequest {

	public Guid Id { get; init; }

}
