using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Olympus.Api.Documentation;

public class ParametersOperationFilter : IOperationFilter {

	public void Apply(OpenApiOperation operation, OperationFilterContext context) {

		var apiDescription = context.ApiDescription;

		operation.Deprecated |= apiDescription.IsDeprecated();

		if (operation.Parameters is null) return;

		foreach (var parameter in operation.Parameters) {

			switch (parameter.Name) {

				case "key":
					parameter.Description = "Primary key to look for.";
					break;

				case "api-version":
					parameter.Description = "Intended API version.";
					break;

				case "if-match":
					parameter.Description = "ETag for matching concurrency check.";
					break;

				case "if-none-match":
					parameter.Description = "ETag for non-matching concurrency check.";
					break;

				default:
					var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
					parameter.Description ??= description.ModelMetadata?.Description;
					break;

			}

		}

	}

}
