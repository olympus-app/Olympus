using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Olympus.Api.Identity;

public static class AuthenticationService {

	public static async Task<IResult> LoginAsync([Microsoft.AspNetCore.Mvc.FromBody] UserLoginRequest request, [FromServices] SignInManager<User> signInManager) {

		signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;

		var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: true, lockoutOnFailure: true);

		if (!result.Succeeded) return Results.Problem(ErrorsStrings.Values.LoginFailed, statusCode: StatusCodes.Status401Unauthorized);

		return Results.Ok();

	}

	public static async Task<IResult> LogoutAsync([FromServices] SignInManager<User> signInManager) {

		await signInManager.SignOutAsync();

		return Results.Ok();

	}

	public static async Task<IResult> RegisterAsync([Microsoft.AspNetCore.Mvc.FromBody] UserRegisterRequest request, [FromServices] UserManager<User> userManager) {

		var user = new User() {
			Name = request.Name,
			Email = request.Email,
			UserName = request.Email,
			IsActive = true,
		};

		var result = await userManager.CreateAsync(user, request.Password);

		if (!result.Succeeded) return CreateValidationProblem(result);

		return Results.Created();

	}

	public static IResult Identity(ClaimsPrincipal user) {

		if (user.Identity?.IsAuthenticated != true) return Results.Unauthorized();

		var identity = new UserIdentityResponse() {
			Id = user.GetValue<Guid>(AppClaimsTypes.Id) ?? AppUsers.Unknown.Id,
			Name = user.GetValue(AppClaimsTypes.Name) ?? user.Identity?.Name ?? AppUsers.Unknown.Name,
			Email = user.GetValue(AppClaimsTypes.Email) ?? AppUsers.Unknown.Email,
			UserName = user.GetValue(AppClaimsTypes.UserName),
			JobTitle = user.GetValue(AppClaimsTypes.JobTitle),
			Department = user.GetValue(AppClaimsTypes.Department),
			OfficeLocation = user.GetValue(AppClaimsTypes.OfficeLocation),
			Country = user.GetValue(AppClaimsTypes.Country),
			Roles = user.GetClaims(AppClaimsTypes.Role).Select(claim => claim.Value).ToList(),
			Permissions = user.GetValue(AppClaimsTypes.Permissions),
		};

		return Results.Ok(identity);

	}

	public static IResult ExternalLogin([Microsoft.AspNetCore.Mvc.FromQuery] string provider, [Microsoft.AspNetCore.Mvc.FromQuery] string returnUrl, [FromServices] SignInManager<User> signInManager) {

		var callbackUrl = $"{IdentitySettings.Endpoints.ExternalCallback}?returnUrl={Uri.EscapeDataString(returnUrl)}";

		var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, callbackUrl);

		return Results.Challenge(properties, authenticationSchemes: [provider]);

	}

	public static IResult ExternalLoginCallback(HttpContext context, [Microsoft.AspNetCore.Mvc.FromQuery] string? returnUrl = null, [Microsoft.AspNetCore.Mvc.FromQuery] string? remoteError = null) {

		if (remoteError is not null) return Results.Redirect($"{IdentitySettings.LoginPath}?error={Uri.EscapeDataString(remoteError)}");

		if (context.User.Identity?.IsAuthenticated == true) return Results.LocalRedirect(returnUrl ?? "/");

		return Results.Redirect($"{IdentitySettings.LoginPath}?error=LoginFailed");

	}

	private static IResult CreateValidationProblem(IdentityResult result) {

		var errors = result.Errors.GroupBy(error => error.Code).ToDictionary(group => group.Key, group => group.Select(error => error.Description).ToArray());

		return Results.ValidationProblem(errors);

	}

}
