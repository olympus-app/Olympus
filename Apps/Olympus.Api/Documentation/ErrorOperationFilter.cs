using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Olympus.Api.Documentation;

public class ErrorOperationFilter : IOperationFilter {

	public void Apply(OpenApiOperation operation, OperationFilterContext context) {

		if (operation.Responses is null) return;

		var errorStatusCodes = operation.Responses.Keys.Where(static code => code.StartsWith('4') || code.StartsWith('5')).ToList();
		var errorResponseSchema = context.SchemaGenerator.GenerateSchema(typeof(ErrorResult), context.SchemaRepository);

		foreach (var statusCode in errorStatusCodes) {

			var response = operation.Responses[statusCode];

			response.Content?.Clear();
			response.Content?.Add(ContentTypes.Json, new OpenApiMediaType {
				Schema = errorResponseSchema
			});

		}

	}

}
