namespace Olympus.Api.Host;

public static class ApiHost {

	public static void Main(string[] args) {

		var builder = WebApplication.CreateBuilder(args);

		var info = builder.GetApiHostInfo();

		builder.LoadSettings();

		builder.AddAspireServices();

		builder.AddDiscoveredServices();

		builder.AddApiServices(info);

		builder.AddDatabaseModel();

		builder.BuildAndRun(info);

	}

}
