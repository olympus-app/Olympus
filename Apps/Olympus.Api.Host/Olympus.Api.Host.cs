namespace Olympus.Api.Host;

public sealed class ApiHost {

	public static void Main(string[] args) {

		var builder = WebApplication.CreateBuilder(args);

		builder.AddSettings();

		builder.AddModules();

		builder.AddServices();

		builder.BuildAndRun();

	}

}
