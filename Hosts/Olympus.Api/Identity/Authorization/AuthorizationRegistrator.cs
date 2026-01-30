using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Olympus.Api.Identity;

public static class AuthorizationRegistrator {

	public static void AddAuthorizationServices(this WebApplicationBuilder builder) {

		builder.Services.AddAuthorizationBuilder()
			.SetDefaultPolicy(new AuthorizationPolicyBuilder(IdentityConstants.ApplicationScheme, TokenSettings.SchemeName)
			.RequireAuthenticatedUser()
			.Build());

		builder.Services.AddSingleton<IAuthorizationHandler, PermissionsRequirementsHandler>();

		builder.Services.AddAntiforgery(static options => {
			options.HeaderName = HttpHeaders.Antiforgery;
			options.FormFieldName = HttpHeaders.Antiforgery;
			options.Cookie.Name = IdentitySettings.AntiforgeryCookieName;
			options.Cookie.HttpOnly = true;
			options.Cookie.SameSite = SameSiteMode.Strict;
			options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
		});

	}

	public static void UseAuthorizationMiddleware(this WebApplication app) {

		app.UseAuthorization();

		app.UseMiddleware<AntiforgeryMiddleware>();

	}

}
