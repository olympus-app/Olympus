using Olympus.Server.Authentication;

namespace Olympus.Server.System;

public interface IAuditableEntity {

	public Guid? CreatedBy { get; }
	public DateTime? CreatedAt { get; }
	public User? CreatedUser { get; }

	public Guid? UpdatedBy { get; }
	public DateTime? UpdatedAt { get; }
	public User? UpdatedUser { get; }

	public void SetCreated(Guid? userId, DateTime? createdAt = null);
	public void SetUpdated(Guid? userId, DateTime? updatedAt = null);

}
