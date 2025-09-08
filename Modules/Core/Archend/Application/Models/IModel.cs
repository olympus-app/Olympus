namespace Olympus.Core.Archend;

public interface IModel {

	public Guid Id { get; }

	public UserAudit? CreatedBy { get; }
	public DateTime? CreatedAt { get; }

	public UserAudit? UpdatedBy { get; }
	public DateTime? UpdatedAt { get; }

	public bool Active { get; set; }
	public bool Locked { get; set; }

	public Guid Version { get; set; }

}
