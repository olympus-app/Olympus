namespace Olympus.Generators;

public static class StringExtensions {

	public static string TrimLines(this string value) {

		value = Regex.Replace(value, @"((\r\n)|\r|\n){3,}", "\n\n");
		value = Regex.Replace(value, @"((\r\n)|\r|\n)+$", "\n");

		return value;

	}

}
