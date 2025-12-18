namespace Olympus.Core.Web;

public static class EntityTag {

	public static string From(Guid value) {

		return value.ToString("N").ToUpper();

	}

	public static bool Match(string? one, string? other) {

		if (string.IsNullOrEmpty(one) || string.IsNullOrEmpty(other)) return false;

		return one == other;

	}

	public static bool Match(Guid? one, Guid? other) {

		if (one is null || other is null) return false;

		return one == other;

	}

	public static bool NotMatch(string? one, string? other) {

		if (string.IsNullOrEmpty(one) || string.IsNullOrEmpty(other)) return false;

		return one != other;

	}

	public static bool NotMatch(Guid? one, Guid? other) {

		if (one is null || other is null) return false;

		return one != other;

	}

}
