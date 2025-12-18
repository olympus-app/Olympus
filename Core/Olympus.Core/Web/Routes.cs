namespace Olympus.Core.Web;

public static class Routes {

	public const string Auth = "auth";

	public const string Api = "api";

	public const string ApiDocs = "api/docs";

	public const string ApiRoutes = "api/routes";

	public const string ApiDocsTemplate = "{documentName}.json";

	public const string Web = "";

	public const string Index = "index.html";

	public const string List = "/";

	public const string Create = "/";

	public const string Read = "/{id}";

	public const string Update = "/{id}";

	public const string Delete = "/{id}";

	public static string GetById(string path, Guid id) {

		return path.Replace("{id}", id.ToString());

	}

}
