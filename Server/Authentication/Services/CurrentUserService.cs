using System.Security.Claims;
using Olympus.Server.Statics;

namespace Olympus.Server.Authentication;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService {

	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public bool IsAuthenticated {

		get {

			var user = _httpContextAccessor.HttpContext?.User;

			return user != null && user.Identity != null && user.Identity.IsAuthenticated;

		}

	}

	public Guid UserId {

		get {

			var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(SystemVars.SystemClaim);

			if (Guid.TryParse(userIdClaim, out var userId)) { return userId; }

			return Guid.Empty;

		}

	}

}
