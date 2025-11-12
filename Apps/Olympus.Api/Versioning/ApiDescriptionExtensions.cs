using System.Globalization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Olympus.Api.Versioning;

public static class ApiDescriptionExtensions {

	extension(ApiDescription description) {

		public string GetVersionName() {

			return description.GetApiVersion()?.ToString(AppConsts.ApiVersionFormat, CultureInfo.InvariantCulture) ?? "v1.0";

		}

		public string GetGroupFullName() {

			return description.GroupName ?? string.Empty;

		}

		public string GetGroupBaseName() {

			return description.GetGroupFullName().Split(" - ")[0];

		}

		public string GetDocumentName() {

			return description.GetGroupFullName().Slugify();

		}

		public string GetDocumentPath() {

			return $"{AppConsts.ApiPath}/{description.GetDocumentName()}.json";

		}

		public bool IsModuleGroup() {

			return description.GetGroupFullName().Split(" - ").Length > 1;

		}

	}

}
