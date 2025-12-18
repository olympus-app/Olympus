namespace Olympus.Api.Host;

public static class ApiHost {

	public static void Main(string[] args) {

		var options = new ApiOptions();

		var builder = WebApplication.CreateBuilder(args);

		builder.AddAspire();

		builder.AddSettings();

		builder.AddDatabase();

		builder.AddModules(options);

		builder.AddServices(options);

		builder.BuildAndRun();

	}

}
