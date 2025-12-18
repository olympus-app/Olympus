using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Olympus.Api.Identity;

public class TokenAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, DatabaseContext database, IUserClaimsPrincipalFactory<User> principalFactory) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder) {

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {

		if (!Request.Headers.TryGetValue(Headers.Authorization, out var authorizationHeader)) return AuthenticateResult.NoResult();

		var headerValue = authorizationHeader.ToString();
		if (!headerValue.StartsWith(Headers.Bearer, StringComparison.OrdinalIgnoreCase)) return AuthenticateResult.NoResult();

		var tokenSpan = headerValue.AsSpan(Headers.Bearer.Length).Trim();
		var tokenHash = TokenService.ComputeHash(tokenSpan);

		var apiToken = await database.Set<Token>().AsNoTracking().Cacheable(CacheExpirationMode.Sliding, TimeSpan.FromMinutes(30)).FirstOrDefaultAsync(token => token.Hash == tokenHash);

		if (apiToken?.IsExpired != false || !apiToken.IsActive) return AuthenticateResult.Fail(ErrorsStrings.Values.TokenInvalidExpired);

		var user = await database.Set<User>().Cacheable(CacheExpirationMode.Sliding, TimeSpan.FromMinutes(30)).FirstOrDefaultAsync(user => user.Id == apiToken.UserId);
		if (user?.IsActive != true) return AuthenticateResult.Fail(ErrorsStrings.Values.UserInactiveOrNotFound);

		var principal = await principalFactory.CreateAsync(user);
		if (principal.Identity is ClaimsIdentity identity) identity.AddClaim(new Claim(AppClaimsTypes.TokenId, apiToken.Id.ToString()));

		var ticket = new AuthenticationTicket(principal, TokenSetting.SchemeName);

		return AuthenticateResult.Success(ticket);

	}

}
