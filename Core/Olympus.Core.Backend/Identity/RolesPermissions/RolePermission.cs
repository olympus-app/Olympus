namespace Olympus.Core.Backend.Identity;

public class RolePermission : Entity {

	public Guid RoleId { get; set; }

	public virtual Role Role { get; set; } = null!;

	public Guid PermissionId { get; set; }

	public Permission Permission { get; set; } = null!;

}
