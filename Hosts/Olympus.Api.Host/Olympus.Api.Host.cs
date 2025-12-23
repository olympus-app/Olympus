namespace Olympus.Api.Host;

public static class ApiHost {

	public static void Main(string[] args) {

		var builder = WebApplication.CreateBuilder(args);

		var modules = new AppModulesInfo();

		builder.AddAspire();

		builder.AddSettings();

		builder.AddDatabase();

		builder.AddModules(modules);

		builder.AddServices(modules);

		builder.BuildAndRun();

	}

}
