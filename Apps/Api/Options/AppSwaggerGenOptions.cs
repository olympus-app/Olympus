using System.Text;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Olympus.Api;

public class AppSwaggerGenOptions(IEnumerable<IAppModuleOptions> modules, IApiVersionDescriptionProvider provider, AppSettings settings) : IConfigureOptions<SwaggerGenOptions> {

	private readonly Dictionary<IAppModuleOptions, string> AssemblyNames = modules.ToDictionary(module => module, module => module.GetType().Assembly.GetName().Name!);

	private static readonly string[] XmlFilesToInclude = AppDomain.CurrentDomain.GetAssemblies()
		.Where(assembly => assembly.FullName?.StartsWith(AppConsts.AppName) == true)
		.Select(assembly => Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"))
		.Where(File.Exists)
		.ToArray();

	public void Configure(SwaggerGenOptions options) {

		options.DocumentFilter<DocumentFilter>();
		options.OperationFilter<OperationFilter>();
		options.CustomOperationIds(selector => $"{selector.HttpMethod}_{selector.RelativePath}");
		options.CustomSchemaIds(x => GetSchemaId(x.FullName!));

		foreach (var xmlPath in XmlFilesToInclude) options.IncludeXmlComments(xmlPath, true);

		foreach (var version in provider.ApiVersionDescriptions) options.SwaggerDoc(version.GroupName, CreateInfoForApiVersion(version, settings));

	}

	private string GetSchemaId(string name) {

		var module = modules.FirstOrDefault(module => name.StartsWith(AssemblyNames[module]));

		return module != null ? name.Replace(AssemblyNames[module], module.Name) : name;

	}

	private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription version, AppSettings settings) {

		var info = new OpenApiInfo() {
			Title = settings.Host.Name,
			Version = version.ApiVersion.ToString(),
			Contact = new OpenApiContact() { Name = settings.Author.Name, Email = settings.Author.Email },
			License = new OpenApiLicense() { Name = settings.License.Name, Url = new Uri(settings.License.Link) }
		};

		var description = new StringBuilder(512);

		description.Append("Documentation for ").Append(settings.Host.Name).Append(" v").Append(version.ApiVersion.ToString()).AppendLine(".<br>")
				  .AppendLine("This API uses OData protocol and follows the OData v4 specification.<br>")
				  .AppendLine("You can find more about OData in: <a href=\"https://www.odata.org\">https://www.odata.org.</a><br>");

		if (version.IsDeprecated) description.AppendLine("<br>This version has been deprecated.");

		AppendSunsetPolicyInfo(version.SunsetPolicy, description);

		info.Description = description.ToString();

		return info;

	}

	private static void AppendSunsetPolicyInfo(SunsetPolicy? policy, StringBuilder description) {

		if (policy == null) return;

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
