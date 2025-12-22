namespace Olympus.Core.Web;

public enum CachePolicy {

	/// <summary>
	/// Do not store any part of the response or request.
	/// </summary>
	[Label("no-store")]
	None = 0,

	/// <summary>
	/// Do not use a cached copy without revalidating with the origin server.
	/// </summary>
	[Label("no-cache")]
	Validate = 1,

	/// <summary>
	/// Store the response in private caches only.
	/// </summary>
	[Label("private")]
	Private = 2,

	/// <summary>
	/// Store the response in public caches.
	/// </summary>
	[Label("public")]
	Public = 3,

}
