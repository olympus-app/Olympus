using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace Olympus.Core.Web;

public class AppUriBuilder {

	private string? Protocol;

	private string? SubDomain;

	private string? Domain;

	private string? Host;

	private string? Path;

	private string? Query;

	private string? Fragment;

	public AppUriBuilder(string route) {

		if (string.IsNullOrWhiteSpace(route)) return;

		var hashIndex = route.IndexOf('#');

		if (hashIndex >= 0) {

			Fragment = route[(hashIndex + 1)..];
			route = route[..hashIndex];

		}

		var queryIndex = route.IndexOf('?');

		if (queryIndex >= 0) {

			Query = route[(queryIndex + 1)..];
			route = route[..queryIndex];

		}

		var protocolEnd = route.IndexOf("://", StringComparison.Ordinal);

		if (protocolEnd >= 0) {

			Protocol = route[..protocolEnd];

			var afterProtocol = route[(protocolEnd + 3)..];
			var firstSlash = afterProtocol.IndexOf('/');

			if (firstSlash >= 0) {

				Host = afterProtocol[..firstSlash];
				Path = afterProtocol[firstSlash..];

			} else {

				Host = afterProtocol;
				Path = string.Empty;

			}

			var hostParts = Host.Split('.');

			if (hostParts.Length > 2) {

				SubDomain = hostParts[0];
				Domain = string.Join('.', hostParts[1..]);

			} else {

				SubDomain = null;
				Domain = Host;

			}

		} else {

			Path = route;

		}

	}

	public static AppUriBuilder FromAuth(string? route = null, int? version = 1) {

		return new AppUriBuilder($"{AppRoutes.Auth}/v{(version.HasValue ? version.ToString() : "{version}") + route}");

	}

	public static AppUriBuilder FromApi(string? route = null, int? version = 1) {

		return new AppUriBuilder($"{AppRoutes.Api}/v{(version.HasValue ? version.ToString() : "{version}") + route}");

	}

	public static AppUriBuilder FromWeb(string? route = null) {

		return new AppUriBuilder(AppRoutes.Web + route?.TrimStart('/'));

	}

	public AppUriBuilder ClearProtocol() {

		Protocol = null;

		return this;

	}

	public AppUriBuilder WithProtocol(string? protocol) {

		if (string.IsNullOrWhiteSpace(protocol)) return this;

		Protocol = protocol.Split(':')[0];

		return this;

	}

	public AppUriBuilder ClearSubDomain() {

		SubDomain = null;

		Host = Domain;

		return this;

	}

	public AppUriBuilder WithSubDomain(string? subdomain) {

		if (string.IsNullOrWhiteSpace(subdomain)) return this;

		SubDomain = subdomain;

		if (string.IsNullOrWhiteSpace(Domain)) Domain = "{domain}";

		Host = $"{SubDomain}.{Domain}";

		return this;

	}

	public AppUriBuilder ClearDomain() {

		Domain = null;

		Host = SubDomain;

		return this;

	}

	public AppUriBuilder WithDomain(string? domain) {

		if (string.IsNullOrWhiteSpace(domain)) return this;

		Domain = domain;

		Host = string.IsNullOrEmpty(SubDomain) ? Domain : $"{SubDomain}.{Domain}";

		return this;

	}

	public AppUriBuilder ClearHost() {

		Host = null;

		SubDomain = null;

		Domain = null;

		return this;

	}

	public AppUriBuilder WithHost(string? host) {

		if (string.IsNullOrWhiteSpace(host)) return this;

		Host = host;

		var parts = host.Split('.');

		if (parts.Length > 2) {

			SubDomain = parts[0];
			Domain = string.Join('.', parts[1..]);

		} else {

			SubDomain = null;
			Domain = host;

		}

		return this;

	}

	public AppUriBuilder ClearPath() {

		Path = null;

		return this;

	}

	public AppUriBuilder WithPath(string? path, bool append = true) {

		if (string.IsNullOrWhiteSpace(path)) return this;

		var cleanPath = path.Trim('/');

		if (!append || Path is null) {

			Path = "/" + cleanPath;

		} else {

			var separator = Path.EndsWith('/') ? string.Empty : "/";
			Path = Path + separator + cleanPath;

		}

		return this;

	}

	public AppUriBuilder ClearQuery() {

		Query = null;

		return this;

	}

	public AppUriBuilder WithQuery(string key, string? value, bool append = true) {

		if (string.IsNullOrWhiteSpace(key)) return this;

		if (string.IsNullOrWhiteSpace(value)) return this;

		if (!append) Query = null;

		var encodedValue = Uri.EscapeDataString(value ?? string.Empty);

		var pair = $"{key}={encodedValue}";

		Query = string.IsNullOrEmpty(Query) ? pair : $"{Query}&{pair}";

		return this;

	}

	public AppUriBuilder WithQuery(string key, object? value, bool append = true) {

		if (value is null) return this;

		if (!append) Query = null;

		if (value is not string and IEnumerable list) {

			foreach (var item in list) {

				WithQuery(key, item.ToString(), true);

			}

		} else {

			WithQuery(key, value.ToString(), true);

		}

		return this;

	}

	public AppUriBuilder WithQuery(string? query, bool append = true) {

		if (string.IsNullOrWhiteSpace(query)) return this;

		if (!append) Query = null;

		var querySection = query.Contains('?') ? query.Split(['?'], 2)[1] : query;

		var cleanQuery = querySection.TrimStart('?', '&');

		if (string.IsNullOrWhiteSpace(cleanQuery)) return this;

		Query = string.IsNullOrEmpty(Query) ? cleanQuery : $"{Query}&{cleanQuery}";

		return this;

	}

	public AppUriBuilder WithQuery(object? query, bool append = true, [CallerArgumentExpression(nameof(query))] string? paramName = null) {

		if (query is null) return this;

		if (!append) Query = null;

		var type = query.GetType();

		if (type.IsSimple() && !string.IsNullOrWhiteSpace(paramName)) {

			WithQuery(paramName, query.ToString(), true);

		} else {

			foreach (var prop in type.GetProperties()) {

				var value = prop.GetValue(query);

				if (value is not null) WithQuery(prop.Name, value.ToString());

			}

		}

		return this;

	}

	public AppUriBuilder ClearFragment() {

		Fragment = null;

		return this;

	}

	public AppUriBuilder WithFragment(string? fragment) {

		if (string.IsNullOrWhiteSpace(fragment)) return this;

		Fragment = fragment.TrimStart('#');

		return this;

	}

	public AppUriBuilder WithBaseAddress(string? baseAddress) {

		if (string.IsNullOrWhiteSpace(baseAddress)) return this;

		var baseRoute = new AppUriBuilder(baseAddress);

		if (!string.IsNullOrEmpty(baseRoute.Protocol)) Protocol = baseRoute.Protocol;

		if (!string.IsNullOrEmpty(baseRoute.SubDomain)) SubDomain = baseRoute.SubDomain;

		if (!string.IsNullOrEmpty(baseRoute.Domain)) Domain = baseRoute.Domain;

		if (!string.IsNullOrEmpty(baseRoute.Host)) Host = baseRoute.Host;

		var currentPath = Path;

		if (currentPath is not null && !currentPath.StartsWith('/')) currentPath = "/" + currentPath;

		Path = (baseRoute.Path?.TrimEnd('/') ?? string.Empty) + currentPath;

		if (!string.IsNullOrEmpty(baseRoute.Query)) {

			Query = string.IsNullOrEmpty(Query) ? baseRoute.Query : $"{baseRoute.Query}&{Query}";

		}

		return this;

	}

	public AppUriBuilder WithPlaceholder(string name, object? value) {

		if (value is null || value is string str && string.IsNullOrEmpty(str)) return this;

		var placeholder = name.StartsWith('{') && name.EndsWith('}') ? name : $"{{{name}}}";
		var valueString = value.ToString() ?? string.Empty;

		if (Protocol?.Contains(placeholder) == true) Protocol = Protocol.Replace(placeholder, valueString);
		if (SubDomain?.Contains(placeholder) == true) SubDomain = SubDomain.Replace(placeholder, valueString);
		if (Domain?.Contains(placeholder) == true) Domain = Domain.Replace(placeholder, valueString);
		if (Host?.Contains(placeholder) == true) Host = Host.Replace(placeholder, valueString);
		if (Path?.Contains(placeholder) == true) Path = Path.Replace(placeholder, valueString);
		if (Fragment?.Contains(placeholder) == true) Fragment = Fragment.Replace(placeholder, valueString);
		if (Query?.Contains(placeholder) == true) Query = Query.Replace(placeholder, valueString);

		return this;

	}

	public AppUriBuilder ClearAll() {

		return ClearProtocol().ClearSubDomain().ClearDomain().ClearHost().ClearPath().ClearQuery().ClearFragment();

	}

	public string Build() {

		var builder = new StringBuilder();

		if (!string.IsNullOrEmpty(Protocol)) builder.Append(Protocol).Append("://");

		if (!string.IsNullOrEmpty(SubDomain)) {

			builder.Append(SubDomain);

			if (!string.IsNullOrEmpty(Domain)) builder.Append('.');

		}

		if (!string.IsNullOrEmpty(Domain)) builder.Append(Domain);

		if (!string.IsNullOrEmpty(Path)) {

			var cleanPath = Path.TrimStart('/');

			var needSlash = builder.Length == 0 || builder[^1] != '/';

			if (needSlash) builder.Append('/');

			builder.Append(cleanPath);

		}

		if (!string.IsNullOrEmpty(Query)) builder.Append('?').Append(Query);

		if (!string.IsNullOrEmpty(Fragment)) builder.Append('#').Append(Fragment);

		return builder.ToString();

	}

	public static implicit operator string(AppUriBuilder builder) => builder.Build();

	public string AsString() => Build();

	public string AsUrl() => Build();

	public Uri AsUri() => new(Build());

	public override string ToString() => Build();

	public string TrimStart() => Build().TrimStart('/');

	public string TrimEnd() => Build().TrimEnd('/');

	public string Trim() => Build().Trim('/');

}
