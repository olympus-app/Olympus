using System.Text;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Olympus.Api.Documentation;

public class SwaggerGenConfigurator(IApiVersionDescriptionProvider provider, AppSettings settings) : IConfigureOptions<SwaggerGenOptions> {

	private static readonly string[] XmlFilesToInclude = AppDomain.CurrentDomain.GetAssemblies()
		.Where(assembly => assembly.FullName?.StartsWith(AppConsts.AppName) == true)
		.Select(assembly => Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"))
		.Where(File.Exists)
		.ToArray();

	public void Configure(SwaggerGenOptions options) {

		options.OperationFilter<ErrorOperationFilter>();
		options.OperationFilter<ParametersOperationFilter>();
		// options.OperationFilter<ODataMetadataOperationFilter>();
		// options.SchemaFilter<ODataSingleResponseSchemaFilter>();
		// options.SchemaFilter<ODataPagedResponseSchemaFilter>();
		options.DocumentFilter<ODataCountDocumentFilter>();
		options.CustomOperationIds(x => $"{x.HttpMethod}_{x.RelativePath}");

		foreach (var xmlPath in XmlFilesToInclude) options.IncludeXmlComments(xmlPath, true);

		foreach (var version in provider.ApiVersionDescriptions) {

			if (version.IsModuleGroup()) {

				options.SwaggerDoc(version.GetDocumentName(), CreateInfoForApiVersion(version, settings));
				options.DocInclusionPredicate((docName, apiDesc) => docName == apiDesc.GetDocumentName());

			}

		}

	}

	private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription version, AppSettings settings) {

		var info = new OpenApiInfo() {
			Title = $"{settings.Host.Name} ({version.GetGroupBaseName()})",
			Version = version.GetVersionName(),
			Contact = new OpenApiContact() { Name = settings.Author.Name, Email = settings.Author.Email },
			License = new OpenApiLicense() { Name = settings.License.Name, Url = new Uri(settings.License.Link) }
		};

		var description = new StringBuilder(512);

		description.Append("Documentation for ").Append(info.Title).Append(' ').Append(info.Version).AppendLine(".<br>")
				  .AppendLine("This API uses OData protocol and follows the OData v4 specification.<br>")
				  .AppendLine("You can find more about OData in: <a href=\"https://www.odata.org\">https://www.odata.org.</a><br>");

		if (version.IsDeprecated) description.AppendLine("<br>This version has been deprecated.");

		AppendSunsetPolicyInfo(version.SunsetPolicy, description);

		info.Description = description.ToString();

		return info;

	}

	private static void AppendSunsetPolicyInfo(SunsetPolicy? policy, StringBuilder description) {

		if (policy is null) return;

		if (policy.Date is { } when) description.Append(" And will be sunset on ").Append(when.Date.ToShortDateString()).Append('.');

		if (!policy.HasLinks) return;

		description.AppendLine("<br>");

		var hasRenderedLinks = false;

		foreach (var link in policy.Links.Where(l => l.Type == "text/html")) {

			if (!hasRenderedLinks) {

				description.Append("<h4>Links</h4><ul>");
				hasRenderedLinks = true;

			}

			var linkText = StringSegment.IsNullOrEmpty(link.Title) ? link.LinkTarget.OriginalString : link.Title.ToString();

			description.Append("<li><a href=\"").Append(link.LinkTarget.OriginalString).Append("\">").Append(linkText).Append("</a></li>");

		}

		if (hasRenderedLinks) description.Append("</ul>");

	}

}
