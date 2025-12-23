using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Olympus.Api.Identity;

public static class AuthenticationRegistrator {

	public static void AddAuthenticationServices(this WebApplicationBuilder builder) {

		builder.Services.AddIdentity<User, Role>(static options => {

			options.User.RequireUniqueEmail = true;

			options.Password.RequiredLength = 8;
			options.Password.RequireDigit = true;
			options.Password.RequireUppercase = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireNonAlphanumeric = true;

			options.SignIn.RequireConfirmedEmail = false;
			options.SignIn.RequireConfirmedAccount = false;
			options.SignIn.RequireConfirmedPhoneNumber = false;

			options.ClaimsIdentity.UserIdClaimType = AppClaimsTypes.Id;
			options.ClaimsIdentity.UserNameClaimType = AppClaimsTypes.Name;
			options.ClaimsIdentity.EmailClaimType = AppClaimsTypes.Email;
			options.ClaimsIdentity.RoleClaimType = AppClaimsTypes.Role;
			options.ClaimsIdentity.SecurityStampClaimType = AppClaimsTypes.SecurityStamp;

			options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;

		}).AddDefaultTokenProviders().AddClaimsPrincipalFactory<ClaimsPrincipalFactory>().AddEntityFrameworkStores<EntityDatabase>();

		builder.Services.ConfigureApplicationCookie(static options => {

			options.Cookie.Name = IdentitySettings.CookieName;
			options.Cookie.HttpOnly = true;
			options.Cookie.SameSite = SameSiteMode.Lax;
			options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

			options.SlidingExpiration = true;
			options.ExpireTimeSpan = TimeSpan.FromDays(30);

			options.Events.OnRedirectToLogin = static context => {
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				return Task.CompletedTask;
			};

			options.Events.OnRedirectToAccessDenied = static context => {
				context.Response.StatusCode = StatusCodes.Status403Forbidden;
				return Task.CompletedTask;
			};

		});

		builder.Services.Configure<SecurityStampValidatorOptions>(static options => options.ValidationInterval = TimeSpan.FromMinutes(30));

		builder.Services.AddAuthentication()
			.AddOpenIdConnect(IdentityProviderType.MicrosoftBusiness.Name, IdentityProviderType.MicrosoftBusiness.Humanize(LetterCasing.Title), static options => { })
			.AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>(TokenSettings.SchemeName, static options => { });

		builder.Services.ConfigureOptions<MicrosoftBusinessProviderOptions>();

		builder.Services.AddTransient<IEntityTable, TokenTable>();

	}

	public static void UseAuthenticationMiddleware(this WebApplication app) {

		app.UseAuthentication();

		app.UseMiddleware<ClaimsPrincipalMiddleware>();

	}

}
