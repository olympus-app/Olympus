using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class User : IdentityUser<Guid>, IEntity {

	public string Name { get; set; } = string.Empty;

	public string? Title { get; set; }

	public Guid? PhotoId { get; set; }

	public virtual StorageImage? Photo { get; set; }

	public string? PhotoUrl => Photo is null ? null : AppUriBuilder.FromApi(CoreRoutes.Users.Photo).WithId(Id).WithCacheBusting(Photo.UpdatedAt).AsString();

	public virtual ICollection<UserClaim> Claims { get; set; } = [];

	public virtual ICollection<UserRole> Roles { get; set; } = [];

	public virtual ICollection<UserPermission> Permissions { get; set; } = [];

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
