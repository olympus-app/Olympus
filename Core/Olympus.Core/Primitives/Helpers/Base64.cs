using System.Text;

namespace Olympus.Core.Primitives;

public static class Base64 {

	public static string ToBase64Url(byte[] value) {

		return Convert.ToBase64String(value).TrimEnd('=').Replace('+', '-').Replace('/', '_');

	}

	public static string ToBase64Url(string value) {

		var bytes = Encoding.UTF8.GetBytes(value);

		return ToBase64Url(bytes);

	}

	public static byte[] BytesFromBase64Url(string value) {

		var length = value.Length + (4 - value.Length % 4) % 4;

		var transform = value.PadRight(length, '=').Replace('-', '+').Replace('_', '/');

		return Convert.FromBase64String(transform);

	}

	public static string StringFromBase64Url(string value) {

		var bytes = BytesFromBase64Url(value);

		return Encoding.UTF8.GetString(bytes);

	}

}
