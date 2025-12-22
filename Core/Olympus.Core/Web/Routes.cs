using System.Collections;

namespace Olympus.Core.Web;

public static class Routes {

	public const string Auth = "auth";

	public const string Api = "api";

	public const string ApiDocs = "api/docs";

	public const string ApiRoutes = "api/routes";

	public const string ApiDocsTemplate = "{documentName}.json";

	public const string Web = "";

	public const string Index = "index.html";

	public const string List = "/";

	public const string Query = "/";

	public const string Create = "/";

	public const string Read = "/{id}";

	public const string Update = "/{id}";

	public const string Delete = "/{id}";

	public static RouteBuilder FromAuth() {

		return new RouteBuilder($"/{Auth}");

	}

	public static RouteBuilder FromApi(int? version = null) {

		var segment = version.HasValue ? version.ToString() : "{version}";

		return new RouteBuilder($"/{Api}/v{segment}");

	}

	public static RouteBuilder FromWeb() {

		return new RouteBuilder($"/{Web}");

	}

	public struct RouteBuilder(string path) {

		private string Path = path;

		public RouteBuilder Append(string segment) {

			if (string.IsNullOrWhiteSpace(segment)) return this;

			var fragmentParts = Path.Split('#');
			var urlWithoutFragment = fragmentParts[0];
			var fragment = fragmentParts.Length > 1 ? "#" + fragmentParts[1] : string.Empty;

			var queryParts = urlWithoutFragment.Split('?');
			var basePath = queryParts[0];
			var query = queryParts.Length > 1 ? "?" + queryParts[1] : string.Empty;

			var cleanSegment = segment.Trim('/');
			var separator = basePath.EndsWith('/') ? string.Empty : "/";

			Path = basePath + separator + cleanSegment + query + fragment;

			return this;

		}

		public RouteBuilder WithId(object value) {

			if (value is null) return this;

			Path = Path.Replace("{id}", value.ToString());

			return this;

		}

		public RouteBuilder WithParam(string name, object value) {

			if (value is null) return this;

			var placeholder = name.StartsWith('{') && name.EndsWith('}') ? name : $"{{{name}}}";

			Path = Path.Replace(placeholder, value.ToString());

			return this;

		}

		public RouteBuilder WithQuery(string key, object value) {

			if (value is null) return this;

			if (value is not string and IEnumerable list) {

				foreach (var item in list) WithQuery(key, item);

				return this;

			}

			var parts = Path.Split('#');
			var core = parts[0];
			var fragment = parts.Length > 1 ? "#" + parts[1] : string.Empty;
			var separator = core.Contains('?') ? "&" : "?";

			core += $"{separator}{key}={Uri.EscapeDataString(value.ToString() ?? string.Empty)}";

			Path = core + fragment;

			return this;

		}

		public RouteBuilder WithQuery(object query) {

			if (query is null) return this;

			foreach (var prop in query.GetType().GetProperties()) {

				var value = prop.GetValue(query);

				if (value is not null) WithQuery(prop.Name, value);

			}

			return this;

		}

		public RouteBuilder WithCacheBusting(object value) {

			if (value is null) return this;

			return WithQuery("v", value);

		}

		public RouteBuilder WithCacheBusting(DateTime? value) {

			if (value is null) return this;

			return WithQuery("v", value.Value.Ticks.ToString("X"));

		}

		public RouteBuilder WithCacheBusting(DateTimeOffset? value) {

			if (value is null) return this;

			return WithQuery("v", value.Value.Ticks.ToString("X"));

		}

		public RouteBuilder WithFragment(string fragment) {

			if (string.IsNullOrWhiteSpace(fragment)) return this;

			var parts = Path.Split('#');

			Path = parts[0] + "#" + fragment;

			return this;

		}

		public static implicit operator string(RouteBuilder builder) => builder.Path;

		public override readonly string ToString() => Path;

	}

}
