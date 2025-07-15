using System.Security.Claims;
using Olympus.Server.Database;

namespace Olympus.Server.Authentication;

public interface IUserProvider {

	public bool CanHandle(ClaimsPrincipal principal);
	public Task<User> ProvisionUserAsync(ClaimsPrincipal principal, DefaultDatabaseContext dbContext);

}
