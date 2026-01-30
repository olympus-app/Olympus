namespace Olympus.Api.Documentation;

public static class DocumentationRegistrator {

	public static void AddDocumentationServices(this WebApplicationBuilder builder, ApiHostInfo info) {

		foreach (var module in info.ModulesInfo) {

			foreach (var version in module.ApiVersions) {

				builder.Services.SwaggerDocument(options => {

					options.DocumentSettings = settings => {
						settings.DocumentName = $"{module.CodeName} - v{version}";
						settings.Title = $"Olympus {module.CodeName} API";
						settings.Version = $"v{version}";
					};

					options.AutoTagPathSegmentIndex = 2;
					options.EnableJWTBearerAuth = false;
					options.ExcludeNonFastEndpoints = true;
					options.FlattenSchema = true;
					options.ReleaseVersion = version;
					options.RemoveEmptyRequestSchema = true;
					options.ShortSchemaNames = true;
					options.ShowDeprecatedOps = true;

					options.EndpointFilter = definition => {

						var endpointModuleName = definition.EndpointType.GetModuleName();

						if (!string.Equals(endpointModuleName, module.CodeName, StringComparison.OrdinalIgnoreCase)) return false;

						return definition.Version.Current == version;

					};

				});

			}

		}

	}

	public static void MapDocumentation(this WebApplication app) {

		app.UseSwaggerGen(
			static options => options.Path = $"{AppRoutes.ApiDocs}/{AppRoutes.ApiDocsTemplate}",
			static options => {
				options.Path = AppRoutes.ApiDocs;
				options.DocumentPath = $"{AppRoutes.ApiDocs}/{AppRoutes.ApiDocsTemplate}";
				options.CustomInlineStyles = ".scheme-container{display:none}";
				options.CustomHeadContent = string.Empty;
				options.EnableTryItOut = false;
				options.DocExpansion = "list";
				options.DefaultModelExpandDepth = 1;
				options.DefaultModelsExpandDepth = 0;
				options.ShowOperationIDs();
			}
		);

	}

}
