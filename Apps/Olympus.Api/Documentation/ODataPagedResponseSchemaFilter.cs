using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Olympus.Api.Documentation;

public class ODataPagedResponseSchemaFilter : ISchemaFilter {

	public void Apply(IOpenApiSchema schema, SchemaFilterContext context) {

		if (!context.Type.IsGenericType || context.Type.GetGenericTypeDefinition() != typeof(ODataPageNoMetadataResult<>) && context.Type.GetGenericTypeDefinition() != typeof(ODataPageMinimalMetadataResult<>)) return;

		if (schema.Properties is null) return;

		var valueProperty = schema.Properties.FirstOrDefault(p => p.Key.Equals("value", StringComparison.OrdinalIgnoreCase)).Value;
		// if (valueProperty?.Items?.Reference?.Id == null) return;

		// var originalModelSchema = context.SchemaRepository.Schemas[valueProperty.Items.Reference.Id];

		// var newProperties = new Dictionary<string, OpenApiSchema> { ["@odata.etag"] = new OpenApiSchema { Type = "string" } };

		// if (originalModelSchema.Properties != null) {

		// 	foreach (var property in originalModelSchema.Properties) {

		// 		newProperties[property.Key] = property.Value;

		// 	}

		// }

		// var itemsSchema = new OpenApiSchema { Type = "object", Properties = newProperties };

		// valueProperty.Items = itemsSchema;

	}

}
