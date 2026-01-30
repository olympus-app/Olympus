namespace Olympus.Core.Web;

public enum CacheLocation {

	/// <summary>
	/// Do not store the response.
	/// </summary>
	[Label("no-store")]
	None = 0,

	/// <summary>
	/// Store the response in private caches only.
	/// </summary>
	[Label("private")]
	Private = 1,

	/// <summary>
	/// Store the response in public caches.
	/// </summary>
	[Label("public")]
	Public = 2,

}
