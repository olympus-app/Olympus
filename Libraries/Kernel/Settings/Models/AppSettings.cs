namespace Olympus.Kernel;

public class AppSettings {

	public HostInfo Host { get; set; } = new();
	public AuthorInfo Author { get; set; } = new();
	public CompanyInfo Company { get; set; } = new();
	public LicenseInfo License { get; set; } = new();

	public class HostInfo {

		public string Name { get; set; } = "Unknown";
		public string Type { get; set; } = "Unknown";

		public AppEnvironment Environment { get; set; } = AppEnvironment.Unknown;

	}

	public class AuthorInfo {

		public string Name { get; set; } = "Éwerton Ferreira";
		public string Email { get; set; } = "ewerton-ferreira@outlook.com";
		public string Phone { get; set; } = "(00) 98765-4321";

	}

	public class CompanyInfo {

		public string Name { get; set; } = "Olympus";
		public string Domain { get; set; } = "olympus.app.br";
		public string WebSite { get; set; } = "https://www.olympus.app.br";

	}

	public class LicenseInfo {

		public string Name { get; set; } = "All Rights Reserved";
		public string Link { get; set; } = "https://olympus.app.com/license";

	}

}
