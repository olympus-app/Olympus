namespace Olympus.Core.Archend.Entities;

public interface IEntityDeleteRequest : IEntityRequest {

	public Guid Id { get; init; }

	public Guid? RowVersion { get; init; }

	public bool Force { get; init; }

}
