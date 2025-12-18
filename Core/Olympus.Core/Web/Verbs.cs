namespace Olympus.Core.Web;

public static class Verbs {

	public const string Head = "HEAD";

	public const string Options = "OPTIONS";

	public const string Get = "GET";

	public const string Post = "POST";

	public const string Put = "PUT";

	public const string Patch = "PATCH";

	public const string Delete = "DELETE";

	public static readonly HashSet<string> SafeVerbs = new(StringComparer.OrdinalIgnoreCase) { Head, Options, Get };

	public static readonly HashSet<string> UnsafeVerbs = new(StringComparer.OrdinalIgnoreCase) { Post, Put, Patch, Delete };

}
