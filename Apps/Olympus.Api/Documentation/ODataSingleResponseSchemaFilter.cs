using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Olympus.Api.Documentation;

public class ODataSingleResponseSchemaFilter : ISchemaFilter {

	public void Apply(IOpenApiSchema schema, SchemaFilterContext context) {

		if (!context.Type.IsGenericType || context.Type.GetGenericTypeDefinition() != typeof(ODataItemNoMetadataResult<>) && context.Type.GetGenericTypeDefinition() != typeof(ODataItemMinimalMetadataResult<>)) return;

		var modelType = context.Type.GetGenericArguments()[0];

		if (!context.SchemaRepository.TryLookupByType(modelType, out var modelSchemaRef)) return;

		if (modelSchemaRef.Reference?.Id is null) return;

		var modelSchema = context.SchemaRepository.Schemas[modelSchemaRef.Reference.Id];

		if (modelSchema.Properties is not null) {

			foreach (var property in modelSchema.Properties) {

				schema.Properties?.Add(property.Key, property.Value);

			}

		}

	}

}
