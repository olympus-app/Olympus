namespace Olympus.Core.Archend.Entities;

public interface IEntityUpdateRequest : IEntityRequest {

	public Guid Id { get; init; }

	public Guid? RowVersion { get; init; }

	public bool IsActive { get; set; }

}
