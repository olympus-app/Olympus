namespace Olympus.Core.Web;

public static class ResponseCache {

	/// <summary>
	/// Do not store the response.
	/// </summary>
	public const string None = "no-store";

	/// <summary>
	/// Store the response in private caches only.
	/// </summary>
	public const string Private = "private";

	/// <summary>
	/// Store the response in private caches only and revalidate before using it.
	/// </summary>
	public const string PrivateRevalidate = "private, no-cache";

	/// <summary>
	/// Store the response in public caches.
	/// </summary>
	public const string Public = "public";

	/// <summary>
	/// Store the response in public caches and revalidate before using it.
	/// </summary>
	public const string PublicRevalidate = "public, no-cache";

	/// <summary>
	/// Builds a Cache-Control header value based on cache location, policy, and duration.
	/// </summary>
	/// <remarks>
	/// When <paramref name="location"/> is <c>None</c>, both <paramref name="policy"/> and <paramref name="duration"/> are ignored.
	/// </remarks>
	/// <param name="location">
	/// Defines where the response may be cached (none, private, or public).
	/// </param>
	/// <param name="policy">
	/// Defines how the cached response should be used (no policy, revalidate or immutable).
	/// </param>
	/// <param name="duration">
	/// Defines the maximum time the response may be reused, in seconds.
	/// </param>
	/// <returns>
	/// A valid Cache-Control header string representing the given configuration.
	/// </returns>
	public static string From(CacheLocation location, CachePolicy policy, TimeSpan? duration = null) {

		var seconds = duration?.TotalSeconds ?? 0;

		var maxage = location != CacheLocation.None && seconds > 0 ? $", max-age={seconds}" : string.Empty;

		var usage = location != CacheLocation.None && policy != CachePolicy.None ? $", {policy.Label}" : string.Empty;

		return location.Label + maxage + usage;

	}

}
