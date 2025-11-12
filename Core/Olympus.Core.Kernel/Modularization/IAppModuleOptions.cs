namespace Olympus.Core.Kernel.Modularization;

public interface IAppModuleOptions {

	public string Name { get; }

	public string Path { get; }

	public string ApiPath { get; }

	public string ApiPrefix { get; }

}
