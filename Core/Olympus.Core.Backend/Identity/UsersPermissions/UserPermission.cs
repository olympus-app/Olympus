namespace Olympus.Core.Backend.Identity;

public class UserPermission : Entity {

	public Guid UserId { get; set; }

	public virtual User User { get; set; } = null!;

	public Guid PermissionId { get; set; }

	public Permission Permission { get; set; } = null!;

}
