namespace Olympus.Core.Archend.Storage;

public interface IStorageEntityLinkRequest : IEntityRequest {

	public Guid Id { get; init; }

}
