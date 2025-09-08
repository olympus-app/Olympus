namespace Olympus.Shared;

public interface IAppModuleOptions {

	public AppModule Enum { get; }

	public string Name { get; set; }

	public string Path { get; set; }

	public string ApiPath { get; }

	public string ApiPrefix { get; }

}
