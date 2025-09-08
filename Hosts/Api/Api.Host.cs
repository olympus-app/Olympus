namespace Olympus.Api.Host;

public sealed class ApiHost {

	public static void Main(string[] args) {

		var builder = WebApplication.CreateBuilder(args);

		builder.AddAppSettings();

		builder.AddModules();

		builder.AddPostgreSql();

		builder.AddServices();

		builder.Run();

	}

}
