namespace Olympus.Core.Settings;

public class IdentitySettings {

	public const string SectionName = "Identity";

	public const string GroupName = "Identity";

	public const string CookieName = "Olympus.Authentication";

	public const string AntiforgeryCookieName = "Olympus.Antiforgery";

	public const string ApiPath = "/auth";

	public const string LoginPath = "/login";

	public static class Endpoints {

		public const string Login = ApiPath + "/login";

		public const string Logout = ApiPath + "/logout";

		public const string Register = ApiPath + "/register";

		public const string Identity = ApiPath + "/identity";

		public const string Cookie = ApiPath + "/cookie";

		public const string ExternalLogin = ApiPath + "/external-login";

		public const string ExternalCallback = ApiPath + "/external-callback";

		public const string Antiforgery = ApiPath + "/antiforgery";

	}

	public IdentityProviderSettings Providers { get; } = new();

}

public class IdentityProviderSettings {

	public const string SectionName = "Identity:Providers";

	public MicrosoftBusinessSettings MicrosoftBusiness { get; } = new();

}

public class MicrosoftBusinessSettings {

	public const string SectionName = "Identity:Providers:MicrosoftBusiness";

	public string Domain { get; set; } = string.Empty;

	public string TenantId { get; set; } = string.Empty;

	public string ClientId { get; set; } = string.Empty;

	public string ClientSecret { get; set; } = string.Empty;

	public string CallbackPath { get; set; } = "/signin-microsoft";

}
