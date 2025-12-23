using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Olympus.Api.Identity;

public static class TokenService {

	public static async Task<IResult> ListAsync(ClaimsPrincipal principal, [FromServices] EntityDatabase database) {

		var userId = principal.GetValue<Guid>(AppClaimsTypes.Id);
		if (userId is null) return Results.Unauthorized();

		var tokens = database.Set<Token>().AsNoTracking().Where(token => token.UserId == userId);

		var response = await TokenMapper.Project(tokens).ToListAsync();

		return Results.Ok(response);

	}

	public static async Task<IResult> CreateAsync(ClaimsPrincipal principal, [Microsoft.AspNetCore.Mvc.FromBody] TokenCreateRequest input, [FromServices] EntityDatabase database) {

		var userId = principal.GetValue<Guid>(AppClaimsTypes.Id);
		if (userId is null) return Results.Unauthorized();

		var (value, hash) = GenerateToken();

		var token = TokenMapper.Create(input);

		token.Hash = hash;
		token.UserId = userId;
		token.Value = string.Create(value.Length, value, static (span, val) => {
			val.AsSpan(0, 3).CopyTo(span);
			span.Slice(3, val.Length - 6).Fill('*');
			val.AsSpan(val.Length - 3, 3).CopyTo(span.Slice(span.Length - 3));
		});

		database.Add(token);
		await database.SaveChangesAsync();

		var response = TokenMapper.Map(token);

		response.Value = value!;

		return Results.Ok(response);

	}

	public static async Task<IResult> DeleteAsync(ClaimsPrincipal principal, [FromRoute] Guid key, [FromServices] EntityDatabase database) {

		var userId = principal.GetValue<Guid>(AppClaimsTypes.Id);
		if (userId is null) return Results.Unauthorized();

		var token = await database.Set<Token>().FirstOrDefaultAsync(token => token.Id == key && token.UserId == userId);

		if (token is null) return Results.NotFound();

		database.Remove(token);
		await database.SaveChangesAsync();

		return Results.NoContent();

	}

	public static string ComputeHash(string input) => ComputeHash(input.AsSpan());

	public static string ComputeHash(ReadOnlySpan<char> input) {

		Span<byte> buffer = stackalloc byte[input.Length * 3];
		var written = Encoding.UTF8.GetBytes(input, buffer);

		Span<byte> hashBuffer = stackalloc byte[32];
		SHA256.HashData(buffer[..written], hashBuffer);

		return Convert.ToBase64String(hashBuffer);

	}

	private static (string Value, string Hash) GenerateToken() {

		var bytes = RandomNumberGenerator.GetBytes(32);
		var value = Base64.ToBase64Url(bytes);
		var hash = ComputeHash(value);

		return (value, hash);

	}

}
