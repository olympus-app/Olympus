using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Olympus.Shared.Utils;

public static class Validator {

	public static bool IsValidString(string value) {

		return !string.IsNullOrWhiteSpace(value);

	}

	public static string ValidateString(string value, [CallerArgumentExpression(nameof(value))] string? name = null) {

		name = name is null ? "String" : Normalizer.NormalizeName(name);
		if (!IsValidString(value)) throw new ArgumentException($"{name} cannot be empty.");

		return Normalizer.NormalizeString(value);

	}

	public static bool IsValidName(string value) {

		return IsValidString(value);

	}

	public static string ValidateName(string value, [CallerArgumentExpression(nameof(value))] string? name = null) {

		name = name is null ? "Name" : Normalizer.NormalizeName(name);
		if (!IsValidString(value)) throw new ArgumentException($"{name} is not a valid name.");

		return Normalizer.NormalizeName(value);

	}

	public static bool IsValidEmail(string value) {

		if (!IsValidString(value)) return false;

		var pattern = @"^(?i)[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
		return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);

	}

	public static string ValidateEmail(string value, [CallerArgumentExpression(nameof(value))] string? name = null) {

		name = name is null ? "Email" : Normalizer.NormalizeName(name);
		if (!IsValidEmail(value)) throw new ArgumentException($"{name} is not a valid email address.");

		return Normalizer.NormalizeEmail(value);

	}

}
