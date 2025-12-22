namespace Olympus.Core.Archend.Entities;

public interface IEntityReadRequest : IEntityRequest {

	public Guid Id { get; init; }

	public string? ETag { get; init; }

}
