using System.Globalization;
using Asp.Versioning.ApiExplorer;

namespace Olympus.Api.Versioning;

public static class ApiVersionDescriptionExtensions {

	extension(ApiVersionDescription description) {

		public string GetVersionName() {

			return description.ApiVersion.ToString(AppConsts.ApiVersionFormat, CultureInfo.InvariantCulture);

		}

		public string GetGroupFullName() {

			return description.GroupName;

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
