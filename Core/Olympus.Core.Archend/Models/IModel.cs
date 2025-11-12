namespace Olympus.Core.Archend.Models;

public interface IModel {

	public Guid Id { get; }

	public UserAuditModel? CreatedBy { get; }
	public DateTimeOffset? CreatedAt { get; }

	public UserAuditModel? UpdatedBy { get; }
	public DateTimeOffset? UpdatedAt { get; }

	public bool Active { get; set; }
	public bool Locked { get; set; }

	public Guid Version { get; set; }

}
