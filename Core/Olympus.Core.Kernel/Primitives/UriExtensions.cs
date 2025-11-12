namespace Olympus.Core.Kernel.Primitives;

public static class UriExtensions {

	extension(Uri value) {

		public string Path => $"/{(value.IsAbsoluteUri ? value.AbsolutePath : value.ToString()).Trim('/').Trim()}";

		public string Prefix => value.Path.Trim('/');

		public static Uri Combine(string[] parts, UriKind uriKind) {

			if (parts is null || parts.Length == 0) return new Uri(string.Empty, uriKind);

			var cleanedParts = parts.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => p.Trim('/'));
			var combinedPath = string.Join("/", cleanedParts);

			return new Uri(combinedPath, uriKind);

		}

	}

}
