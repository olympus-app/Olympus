using System.Security.Claims;

namespace Olympus.Core.Backend.Entities;

public static class ClaimsPrincipalExtensions {

	extension(ClaimsPrincipal principal) {

		public User CurrentUser => new() {
			Id = principal.Id,
			Name = principal.Name,
			Email = principal.Email,
			JobTitle = principal.JobTitle,
			Department = principal.Department,
		};

	}

}
