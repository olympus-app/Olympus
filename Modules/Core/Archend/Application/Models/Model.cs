namespace Olympus.Core.Archend;

public abstract partial record Model : IModel {

	public virtual Guid Id { get; set; } = Guid.CreateVersion7();

	public virtual UserAudit? CreatedBy { get; set; }
	public DateTime? CreatedAt { get; set; }

	public virtual UserAudit? UpdatedBy { get; set; }
	public DateTime? UpdatedAt { get; set; }

	public bool Active { get; set; } = true;
	public bool Locked { get; set; } = false;

	public Guid Version { get; set; } = Guid.Empty;

}
