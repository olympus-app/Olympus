using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Olympus.Api;

public class DocumentFilter : IDocumentFilter {

	public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context) {

		var pathsToRemove = swaggerDoc.Paths
			.Where(p => p.Key.EndsWith("/$count", StringComparison.OrdinalIgnoreCase))
			.Select(p => p.Key)
			.ToList();

		foreach (var path in pathsToRemove) {
			swaggerDoc.Paths.Remove(path);
		}

	}

}
