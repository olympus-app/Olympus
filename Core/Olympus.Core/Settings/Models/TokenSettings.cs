namespace Olympus.Core.Settings;

public class TokenSettings : Settings {

	public const string SchemeName = "ApiTokens";

	public const int TokenLength = 32;

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Token", this);

	}

}
