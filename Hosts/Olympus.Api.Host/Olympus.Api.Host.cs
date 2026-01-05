namespace Olympus.Api.Host;

public static class ApiHost {

	public static void Main(string[] args) {

		var builder = WebApplication.CreateBuilder(args);

		var info = new AppHostInfo(typeof(ApiHost).Assembly);

		builder.AddAspire();

		builder.AddSettings();

		builder.AddDatabase();

		builder.AddModules(info);

		builder.AddServices(info);

		builder.BuildAndRun();

	}

}
