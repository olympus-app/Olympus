namespace Olympus.Core.Kernel.Modularization;

public abstract class AppModuleOptions : IAppModuleOptions {

	public abstract string Name { get; set; }

	public abstract string Path { get; set; }

	public string ApiPath => Uri.Combine([AppConsts.ApiPath, Path], UriKind.Relative).Path;

	public string ApiPrefix => Uri.Combine([AppConsts.ApiPath, Path], UriKind.Relative).Prefix;

}
