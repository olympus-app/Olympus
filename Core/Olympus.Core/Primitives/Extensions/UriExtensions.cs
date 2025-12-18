namespace Olympus.Core.Primitives;

public static class UriExtensions {

	extension(Uri uri) {

		public string Path => "/" + (uri.IsAbsoluteUri ? uri.AbsolutePath : uri.OriginalString).Trim('/');

		public string Prefix => uri.Path.Trim('/');

		public static Uri Combine(string[] parts, UriKind uriKind) {

			if (parts is null || parts.Length == 0) return new Uri(string.Empty, uriKind);

			var buidler = new System.Text.StringBuilder();

			foreach (var part in parts) {

				if (string.IsNullOrWhiteSpace(part)) continue;

				if (buidler.Length > 0) buidler.Append('/');

				buidler.Append(part.Trim('/'));

			}

			return new Uri(buidler.ToString(), uriKind);

		}

	}

}
