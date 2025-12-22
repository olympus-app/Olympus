namespace Olympus.Core.Backend.Identity;

public class Permission : Entity {

	public required int Value { get; set; }

	public required string Name { get; set; }

	public required string Description { get; set; }

	public required string Module { get; set; }

	public required string Feature { get; set; }

	public required string Action { get; set; }

	public virtual ICollection<UserPermission> Users { get; set; } = [];

	public virtual ICollection<RolePermission> Roles { get; set; } = [];

}
