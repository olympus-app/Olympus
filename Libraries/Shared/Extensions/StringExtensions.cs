namespace Olympus.Shared;

public static class StringExtensions {

	public static string Crop(this string value, char oldChar) {

		return value.Replace(oldChar, '\0');

	}

	public static string Crop(this string value, string oldValue) {

		return value.Replace(oldValue, string.Empty);

	}

}
