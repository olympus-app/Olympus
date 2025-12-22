using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class UserRole : IdentityUserRole<Guid>, IEntity {

	public Guid Id { get; set; }

	public Guid? ETag { get; set; }

	public virtual User User { get; set; } = null!;

	public virtual Role Role { get; set; } = null!;

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
