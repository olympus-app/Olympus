using Microsoft.AspNetCore.Antiforgery;

namespace Olympus.Api.Identity;

public static class AuthorizationService {

	public static IResult AntiforgeryToken([FromServices] IAntiforgery antiforgery, HttpContext context) {

		var token = antiforgery.GetAndStoreTokens(context);

		var response = new AntiforgeryResponse() {
			Header = token.HeaderName,
			Token = token.RequestToken,
		};

		return Results.Ok(response);

	}

}
