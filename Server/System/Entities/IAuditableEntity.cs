namespace Olympus.Server.System;

public interface IAuditableEntity {

	public Guid? CreatedBy { get; }
	public DateTime? CreatedAt { get; }
	// @TODO: User Navigation Property

	public Guid? UpdatedBy { get; }
	public DateTime? UpdatedAt { get; }
	// @TODO: User Navigation Property

	public void SetCreated(Guid? userId, DateTime? createdAt = null);
	public void SetUpdated(Guid? userId, DateTime? updatedAt = null);

}
