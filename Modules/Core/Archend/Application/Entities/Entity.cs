namespace Olympus.Core.Archend;

public abstract partial class Entity : IEntity {

	public virtual required Guid Id { get; set; } = Guid.CreateVersion7();

	public virtual User? CreatedBy { get; set; }
	public Guid? CreatedById { get; set; }
	public DateTime? CreatedAt { get; set; }

	public virtual User? UpdatedBy { get; set; }
	public Guid? UpdatedById { get; set; }
	public DateTime? UpdatedAt { get; set; }

	public bool Active { get; set; } = true;
	public bool Locked { get; set; } = false;
	public bool Hidden { get; set; } = false;

	public Guid Version { get; set; } = Guid.Empty;

}
