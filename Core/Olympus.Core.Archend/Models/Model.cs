namespace Olympus.Core.Archend.Models;

public abstract partial record Model : IModel {

	public virtual required Guid Id { get; set; } = Guid.NewGuidv7();

	public virtual UserAuditModel? CreatedBy { get; set; }
	public DateTimeOffset? CreatedAt { get; set; }

	public virtual UserAuditModel? UpdatedBy { get; set; }
	public DateTimeOffset? UpdatedAt { get; set; }

	public bool Active { get; set; } = true;
	public bool Locked { get; set; } = false;

	public Guid Version { get; set; } = Guid.Empty;

}
