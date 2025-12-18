using Microsoft.AspNetCore.Identity;

namespace Olympus.Core.Backend.Identity;

public class UserClaim : IdentityUserClaim<Guid> {

	public virtual User User { get; set; } = null!;

}
