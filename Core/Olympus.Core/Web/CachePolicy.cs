namespace Olympus.Core.Web;

public enum CachePolicy {

	/// <summary>
	/// No policy.
	/// </summary>
	[Label("")]
	None = 0,

	/// <summary>
	/// The response must be revalidated.
	/// </summary>
	[Label("no-cache")]
	Revalidate = 1,

	/// <summary>
	/// The response is immutable, no need to revalidate.
	/// </summary>
	[Label("immutable")]
	Immutable = 2,

}
