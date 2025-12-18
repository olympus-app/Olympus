using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace Olympus.Api.Identity;

public static class PasskeyService {

	public static async Task<IResult> ListAsync(ClaimsPrincipal principal, [FromServices] UserManager<User> manager) {

		var user = await manager.GetUserAsync(principal);
		if (user is null) return Results.Unauthorized();

		var passkeys = await manager.GetPasskeysAsync(user);

		var response = passkeys.Select(passkey => new PasskeyListResponse() {
			Id = Base64.ToBase64Url(passkey.CredentialId),
			Name = passkey.Name ?? "Passkey",
		});

		return Results.Ok(response);

	}

	public static async Task<IResult> LoginAsync([Microsoft.AspNetCore.Mvc.FromBody] JsonElement clientResponse, [FromServices] SignInManager<User> signInManager) {

		try {

			var credentialJson = clientResponse.ToString();
			var result = await signInManager.PasskeySignInAsync(credentialJson);

			return result.Succeeded ? Results.Ok() : result.IsLockedOut ? Results.Problem("User locked out.") : Results.Unauthorized();

		} catch (Exception ex) {

			return Results.BadRequest(ex.Message);

		}

	}

	public static async Task<IResult> LoginOptionsAsync([Microsoft.AspNetCore.Mvc.FromQuery] string? username, [FromServices] SignInManager<User> signInManager, [FromServices] UserManager<User> userManager) {

		User? user = null;
		if (!string.IsNullOrEmpty(username)) user = await userManager.FindByNameAsync(username);

		try {

			var optionsJson = await signInManager.MakePasskeyRequestOptionsAsync(user);

			return Results.Text(optionsJson, contentType: ContentTypes.Json);

		} catch (Exception ex) {

			return Results.BadRequest(ex.Message);

		}

	}

	public static async Task<IResult> RegisterAsync(ClaimsPrincipal principal, [Microsoft.AspNetCore.Mvc.FromBody] PasskeyRegisterRequest input, [FromServices] UserManager<User> userManager, [FromServices] SignInManager<User> signInManager) {

		var user = await userManager.GetUserAsync(principal);
		if (user is null) return Results.NotFound();

		if (!string.IsNullOrEmpty(input.Error)) return Results.BadRequest(input.Error);
		if (string.IsNullOrEmpty(input.Credential)) return Results.BadRequest(ErrorsStrings.Values.PasskeyMissingCredential);

		try {

			var attestationResult = await signInManager.PerformPasskeyAttestationAsync(input.Credential);

			if (!attestationResult.Succeeded) return Results.BadRequest($"Attestation failed: {attestationResult.Failure.Message}");

			var saveResult = await userManager.AddOrUpdatePasskeyAsync(user, attestationResult.Passkey);

			if (!saveResult.Succeeded) return Results.BadRequest("Failed to save passkey.");

			return Results.Created();

		} catch (Exception ex) {

			return Results.BadRequest(ex.Message);

		}

	}

	public static async Task<IResult> RegisterOptionsAsync(ClaimsPrincipal principal, [FromServices] UserManager<User> userManager, [FromServices] SignInManager<User> signInManager) {

		var user = await userManager.GetUserAsync(principal);
		if (user is null) return Results.NotFound();

		var passkeyUser = new PasskeyUserEntity() {
			Id = await userManager.GetUserIdAsync(user),
			Name = await userManager.GetUserNameAsync(user) ?? AppUsers.Unknown.Name,
			DisplayName = user.Name!,
		};

		try {

			var optionsJson = await signInManager.MakePasskeyCreationOptionsAsync(passkeyUser);

			return Results.Text(optionsJson, contentType: ContentTypes.Json);

		} catch (Exception ex) {

			return Results.BadRequest(ex.Message);

		}

	}

	public static async Task<IResult> UnregisterAsync(ClaimsPrincipal principal, [FromRoute] string key, [FromServices] UserManager<User> manager) {

		var user = await manager.GetUserAsync(principal);
		if (user is null) return Results.Unauthorized();

		var credentialIdBytes = Base64.BytesFromBase64Url(key);

		var result = await manager.RemovePasskeyAsync(user, credentialIdBytes);

		if (!result.Succeeded) return Results.BadRequest("Failed to remove passkey.");

		return Results.NoContent();

	}

}
