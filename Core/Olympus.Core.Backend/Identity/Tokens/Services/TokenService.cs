using System.Security.Cryptography;
using System.Text;

namespace Olympus.Core.Backend.Identity;

public class TokenService(IDatabaseService database, IHttpContextAccessor accessor) : EntityService<Token>(database, accessor) {

	public static (string Value, string Hash) GenerateToken() {

		var bytes = RandomNumberGenerator.GetBytes(32);
		var value = Base64.ToBase64Url(bytes);
		var hash = ComputeHash(value);

		return (value, hash);

	}

	public static string ComputeHash(string input) {

		return ComputeHash(input.AsSpan());

	}

	public static string ComputeHash(ReadOnlySpan<char> input) {

		Span<byte> buffer = stackalloc byte[input.Length * 3];
		var written = Encoding.UTF8.GetBytes(input, buffer);

		Span<byte> hashBuffer = stackalloc byte[32];
		SHA256.HashData(buffer[..written], hashBuffer);

		return Convert.ToBase64String(hashBuffer);

	}

}
