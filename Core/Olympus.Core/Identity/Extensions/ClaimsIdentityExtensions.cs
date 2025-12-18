using System.Security.Claims;

namespace Olympus.Core.Identity;

public static class ClaimsIdentityExtensions {

	extension(ClaimsIdentity identity) {

		public void AddClaim(string? type, Guid? value) {

			if (value is not null) identity.AddClaim(type, value.ToString());

		}

		public void AddClaim(string? type, string? value) {

			if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(value)) return;

			identity.AddClaim(new Claim(type, value));

		}

		public void AddClaims(string? type, IEnumerable<string?> values) {

			if (string.IsNullOrEmpty(type) || !values.Any()) return;

			foreach (var value in values) {

				if (value is null) continue;

				identity.AddClaim(new Claim(type, value));

			}

		}

	}

}
