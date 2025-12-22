namespace Olympus.Core.Web;

public static class ResponseCache {

	public static string From(CachePolicy location, TimeSpan? duration = null, bool immutable = false) {

		var seconds = duration?.TotalSeconds ?? TimeSpan.FromHours(1).TotalSeconds;

		var maxage = location != CachePolicy.None && seconds > 0 ? $", max-age={seconds}" : string.Empty;

		var immutability = immutable && location != CachePolicy.None ? ", immutable" : string.Empty;

		return location.GetLabel() + maxage + immutability;

	}

}
