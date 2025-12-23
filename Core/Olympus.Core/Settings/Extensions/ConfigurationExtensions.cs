namespace Olympus.Core.Settings;

public static class ConfigurationExtensions {

	private static string FormatEnvironmentVariable(string variable) {

		return variable.Replace("-", "_").Replace(".", "_").ToUpper();

	}

	extension(IConfiguration configuration) {

		public int GetValueFromEnvironment(string variable, int defaultValue = 0) {

			variable = FormatEnvironmentVariable(variable);

			return configuration.GetValue(variable, defaultValue);

		}

		public string GetValueFromEnvironment(string variable, string defaultValue = "") {

			variable = FormatEnvironmentVariable(variable);

			return configuration.GetValue(variable, defaultValue);

		}

		public string GetConnectionString(string name, string defaultValue = "") {

			return configuration.GetValue($"ConnectionStrings:{name}", defaultValue);

		}

	}

}
