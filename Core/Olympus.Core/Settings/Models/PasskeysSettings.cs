namespace Olympus.Core.Settings;

public static class PasskeysSettings {

	public const string ApiPath = IdentitySettings.ApiPath + "/passkeys";

	public static class Endpoints {

		public const string List = ApiPath + "/list";

		public const string Login = ApiPath + "/login";

		public const string Register = ApiPath + "/register";

		public const string Unregister = ApiPath + "/unregister";

		public const string RegisterOptions = ApiPath + "/registeroptions";

		public const string LoginOptions = ApiPath + "/loginoptions";

	}

}
