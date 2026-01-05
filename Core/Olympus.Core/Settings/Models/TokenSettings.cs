namespace Olympus.Core.Settings;

public class TokenSettings : Settings {

	public const string SchemeName = "ApiTokens";

	public const int TokenLength = 32;

	public const string ApiPath = IdentitySettings.ApiPath + "/tokens";

	public static class Endpoints {

		public const string List = ApiPath + "/list";

		public const string Create = ApiPath + "/create";

		public const string Delete = ApiPath + "/delete";

	}

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Token", this);

	}

}
