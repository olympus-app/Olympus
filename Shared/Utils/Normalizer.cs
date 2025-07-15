namespace Olympus.Shared.Utils;

public static class Normalizer {

	public static string NormalizeString(string value) {

		return value.Trim();

	}

	public static string NormalizeEmail(string address) {

		return address.ToLowerInvariant().Trim();

	}

	public static string NormalizeName(string name, string[]? exceptions = null) {

		if (string.IsNullOrWhiteSpace(name)) return string.Empty;

		var defaultExceptions = new[] { "de", "do", "da", "dos", "das", "e" };
		var ex = exceptions ?? defaultExceptions;

		var words = name.Trim().ToLowerInvariant().Split(" ");

		for (int i = 0; i < words.Length; i++) {

			if (i == 0) {

				if (words[i].Length > 0) words[i] = char.ToUpper(words[i][0]) + words[i][1..];
				continue;

			} else {

				if (ex.Contains(words[i])) continue;
				if (words[i].Length > 0) words[i] = char.ToUpper(words[i][0]) + words[i][1..];

			}

		}

		return string.Join(" ", words);

	}

}
