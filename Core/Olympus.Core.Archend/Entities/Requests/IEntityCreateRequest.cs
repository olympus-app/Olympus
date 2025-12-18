namespace Olympus.Core.Archend.Entities;

public interface IEntityCreateRequest : IEntityRequest {

	public Guid Id { get; init; }

	public bool IsActive { get; set; }

}
