using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class RoleClaim : IdentityRoleClaim<Guid> {

	public virtual Role Role { get; set; } = null!;

}
