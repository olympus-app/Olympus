using System.Text;
using Microsoft.AspNetCore.Routing;

namespace Olympus.Api.Routing;

public static class RouteDebugger {

	public static Task DebugAsync(HttpContext context, IEnumerable<EndpointDataSource> dataSources) {

		var builder = new StringBuilder();

		builder.Append(
			@"<html>
				<head>
					<title>Route Debugger</title>
					<style>
						body { font-family: Segoe UI, sans-serif; padding: 20px; background: #f4f4f4; }
						h1 { color: #333; }
						table { border-collapse: collapse; width: 100%; background: white; box-shadow: 0 1px 3px rgba(0,0,0,0.2); }
						th, td { text-align: left; padding: 12px; border-bottom: 1px solid #ddd; }
						th { color: black; }
						tr:hover { background-color: #f1f1f1; }
						.badge { padding: 4px 8px; border-radius: 4px; color: white; font-size: 0.8em; font-weight: bold; }
						.GET { background-color: #28a745; }
						.POST { background-color: #007bff; }
						.PUT { background-color: #ffc107; color: black; }
						.DELETE { background-color: #dc3545; }
						.OTHER { background-color: #6c757d; }
					</style>
				</head>"
		);

		builder.Append("<body><h1>Route Debugger</h1><table><thead><tr><th>Verbs</th><th>Route</th><th>Handler</th></tr></thead><tbody>");

		var endpoints = dataSources.SelectMany(source => source.Endpoints).OfType<RouteEndpoint>().Where(endpoint => {

			var route = endpoint.RoutePattern.RawText;

			if (string.IsNullOrEmpty(route)) return false;

			if (route.Contains("nonfile")) return false;

			if (route.EndsWith(AppRoutes.ApiRoutes, StringComparison.OrdinalIgnoreCase)) return false;

			if (route.StartsWith(AppRoutes.Api, StringComparison.OrdinalIgnoreCase)) return true;

			if (route.StartsWith(AppRoutes.Auth, StringComparison.OrdinalIgnoreCase)) return true;

			return false;

		}).OrderBy(endpoint => !endpoint.RoutePattern.RawText?.StartsWith(AppRoutes.Auth, StringComparison.OrdinalIgnoreCase))
		.ThenBy(endpoint => !endpoint.RoutePattern.RawText?.StartsWith(AppRoutes.Api, StringComparison.OrdinalIgnoreCase))
		.ThenBy(endpoint => endpoint.RoutePattern.RawText);

		foreach (var endpoint in endpoints) {

			var metadata = endpoint.Metadata;
			var route = endpoint.RoutePattern.RawText;
			var verbs = metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods ?? ["ANY"];
			var handler = metadata.GetMetadata<EndpointDefinition>()?.EndpointType.FullName ?? endpoint.DisplayName;

			foreach (var verb in verbs) {

				var badge = verb switch {
					"GET" or "POST" or "PUT" or "DELETE" => verb,
					_ => "OTHER",
				};

				builder.Append(
					$@"<tr>
                        <td><span class='badge {badge}'>{verb}</span></td>
                        <td style='font-family: monospace; font-size: 1.1em;'>{route}</td>
                        <td style='color: #666;'>{handler}</td>
                    </tr>"
				);

			}

		}

		builder.Append("</tbody></table></body></html>");

		context.Response.ContentType = "text/html";

		return context.Response.WriteAsync(builder.ToString());

	}

}
