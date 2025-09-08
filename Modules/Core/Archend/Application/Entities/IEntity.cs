namespace Olympus.Core.Archend;

public interface IEntity {

	public Guid Id { get; }

	public User? CreatedBy { get; }
	public Guid? CreatedById { get; }
	public DateTime? CreatedAt { get; }

	public User? UpdatedBy { get; }
	public Guid? UpdatedById { get; }
	public DateTime? UpdatedAt { get; }

	public bool Active { get; }
	public bool Locked { get; }
	public bool Hidden { get; }

	public Guid Version { get; }

}
