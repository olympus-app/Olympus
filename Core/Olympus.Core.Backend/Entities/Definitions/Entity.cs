namespace Olympus.Core.Backend.Entities;

public abstract class Entity : IEntity {

	public Guid Id { get; set; }

	public Guid? ETag { get; set; }

	public Guid? CreatedById { get; set; }

	public virtual User? CreatedBy { get; set; }

	public DateTimeOffset? CreatedAt { get; set; }

	public Guid? UpdatedById { get; set; }

	public virtual User? UpdatedBy { get; set; }

	public DateTimeOffset? UpdatedAt { get; set; }

	public Guid? DeletedById { get; set; }

	public virtual User? DeletedBy { get; set; }

	public DateTimeOffset? DeletedAt { get; set; }

	public bool IsActive { get; set; } = true;

	public bool IsDeleted { get; set; }

	public bool IsHidden { get; set; }

	public bool IsLocked { get; set; }

	public bool IsSystem { get; set; }

}
