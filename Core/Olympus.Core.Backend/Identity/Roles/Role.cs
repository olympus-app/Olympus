using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class Role : IdentityRole<Guid>, IEntity {

	public override Guid Id { get; set; }

	public override string? Name { get; set; }

	public override string? NormalizedName { get; set; }

	public string? Description { get; set; }

	public virtual ICollection<UserRole> Users { get; set; } = [];

	public virtual ICollection<RoleClaim> Claims { get; set; } = [];

	public virtual ICollection<RolePermission> Permissions { get; set; } = [];

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

	public Guid? ETag {
		get => string.IsNullOrEmpty(ConcurrencyStamp) || !Guid.TryParse(ConcurrencyStamp, out var guid) ? Guid.Empty : guid;
		set => ConcurrencyStamp = value.ToString();
	}

}
