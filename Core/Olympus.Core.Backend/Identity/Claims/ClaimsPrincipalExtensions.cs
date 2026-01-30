using System.Security.Claims;

namespace Olympus.Core.Backend.Identity;

public static class ClaimsPrincipalExtensions {

	extension(ClaimsPrincipal principal) {

		public User AsEntity() {

			return new() {
				Id = principal.Id,
				Name = principal.Name,
				Email = principal.Email,
				Title = principal.Title,
			};

		}

	}

}
