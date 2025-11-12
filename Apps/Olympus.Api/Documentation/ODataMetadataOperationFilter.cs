using System.Collections;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Olympus.Api.Documentation;

public class ODataMetadataOperationFilter : IOperationFilter {

	public void Apply(OpenApiOperation operation, OperationFilterContext context) {

		if (!context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(attribute => attribute is EnableQueryAttribute)) return;

		if (operation.Responses is null) return;

		var response = operation.Responses.FirstOrDefault(response => response.Key == "200");
		if (response.Key is null) return;

		var responseType = context.ApiDescription.SupportedResponseTypes.FirstOrDefault()?.Type;
		if (responseType is null) return;

		var isPagedResponse = typeof(IEnumerable).IsAssignableFrom(responseType) && responseType != typeof(string);

		Type modelType;
		Type wrapperTypeSimple;
		Type wrapperTypeMinimal;
		OpenApiSchema modelSchemaSimple;
		OpenApiSchema modelSchemaMinimal;

		if (isPagedResponse) {

			modelType = responseType.GetGenericArguments()[0];
			wrapperTypeSimple = typeof(ODataPageNoMetadataResult<>).MakeGenericType(modelType);
			wrapperTypeMinimal = typeof(ODataPageMinimalMetadataResult<>).MakeGenericType(modelType);
			modelSchemaSimple = (OpenApiSchema)context.SchemaGenerator.GenerateSchema(wrapperTypeSimple, context.SchemaRepository);
			modelSchemaMinimal = (OpenApiSchema)context.SchemaGenerator.GenerateSchema(wrapperTypeMinimal, context.SchemaRepository);

		} else {

			modelType = responseType;
			wrapperTypeSimple = typeof(ODataItemNoMetadataResult<>).MakeGenericType(modelType);
			wrapperTypeMinimal = typeof(ODataItemMinimalMetadataResult<>).MakeGenericType(modelType);
			modelSchemaSimple = (OpenApiSchema)context.SchemaGenerator.GenerateSchema(wrapperTypeSimple, context.SchemaRepository);
			modelSchemaMinimal = (OpenApiSchema)context.SchemaGenerator.GenerateSchema(wrapperTypeMinimal, context.SchemaRepository);

		}

		if (response.Value.Content is null) return;

		response.Value.Content.Clear();
		response.Value.Content.Add(ContentTypes.ODataMetadataNone, new OpenApiMediaType { Schema = modelSchemaSimple });
		response.Value.Content.Add(ContentTypes.ODataMetadataMinimal, new OpenApiMediaType { Schema = modelSchemaMinimal });

	}

}
