namespace Olympus.Core.Settings;

public static class TokenSetting {

	public const int TokenLength = 32;

	public const string SchemeName = "ApiTokens";

	public const string ApiPath = IdentitySettings.ApiPath + "/tokens";

	public static class Endpoints {

		public const string List = ApiPath + "/list";

		public const string Create = ApiPath + "/create";

		public const string Delete = ApiPath + "/delete";

	}

}
