namespace Olympus.Core.Settings;

public class IdentitySettings : Settings {

	public const string GroupName = "Identity";

	public const string CookieName = "Olympus.Authentication";

	public const string AntiforgeryCookieName = "Olympus.Antiforgery";

	public IdentityProviderSettings Providers { get; } = new();

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Identity", this);

	}

}

public class IdentityProviderSettings : Settings {

	public MicrosoftBusinessSettings MicrosoftBusiness { get; } = new();

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Identity:Providers", this);

	}

}

public class MicrosoftBusinessSettings : Settings {

	public string Domain { get; set; } = string.Empty;

	public string TenantId { get; set; } = string.Empty;

	public string ClientId { get; set; } = string.Empty;

	public string ClientSecret { get; set; } = string.Empty;

	public string CallbackPath { get; set; } = "/signin-microsoft";

	public override void Configure(IConfiguration configuration) {

		configuration.Bind("Identity:Providers:MicrosoftBusiness", this);

	}

}
