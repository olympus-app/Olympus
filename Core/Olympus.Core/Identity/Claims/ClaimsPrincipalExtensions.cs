using System.Security.Claims;

namespace Olympus.Core.Identity;

public static class ClaimsPrincipalExtensions {

	extension(ClaimsPrincipal principal) {

		public static ClaimsPrincipal Unknown => new();

		public Guid Id => principal.GetValue<Guid>(AppClaimsTypes.Id) ?? AppUsers.Unknown.Id;

		public string Name => principal.GetValue(AppClaimsTypes.Name) ?? AppUsers.Unknown.Name;

		public string? UserName => principal.GetValue(AppClaimsTypes.UserName);

		public string? Email => principal.GetValue(AppClaimsTypes.Email);

		public string? Title => principal.GetValue(AppClaimsTypes.Title);

		public string? Photo => principal.GetValue(AppClaimsTypes.Photo);

		public bool IsAuthenticated => principal.Identity?.IsAuthenticated ?? false;

		public IEnumerable<Claim> GetClaims(string claimType) {

			return principal.FindAll(claimType);

		}

		public string? GetValue(string claimType) {

			return principal.FindFirst(claimType)?.Value;

		}

		public T? GetValue<T>(string claimType) where T : struct, IParsable<T> {

			var value = principal.GetValue(claimType);

			return T.TryParse(value, null, out var result) ? result : null;

		}

		public bool CheckPermissions(int? one = null, ReadOnlySpan<int> any = default, ReadOnlySpan<int> all = default) {

			if (one is null && any.IsEmpty && all.IsEmpty) return true;

			if (principal is not AppClaimsPrincipal user) return false;

			if (one.HasValue && !user.Permissions.Contains(one.Value)) return false;

			if (!any.IsEmpty) {

				var found = false;

				foreach (var perm in any) {

					if (user.Permissions.Contains(perm)) {

						found = true;
						break;

					}

				}

				if (!found) return false;

			}

			if (!all.IsEmpty) {

				foreach (var perm in all) {

					if (!user.Permissions.Contains(perm)) return false;

				}

			}

			return true;

		}

	}

}
