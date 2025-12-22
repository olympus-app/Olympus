namespace Olympus.Core.Archend.Entities;

public interface IEntityListResponse : IEntityResponse {

	public Guid Id { get; init; }

	public bool IsActive { get; init; }

	public bool IsLocked { get; init; }

	public bool IsSystem { get; init; }

}
