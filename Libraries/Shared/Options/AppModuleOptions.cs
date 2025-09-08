namespace Olympus.Shared;

public abstract class AppModuleOptions : IAppModuleOptions {

	public abstract AppModule Enum { get; protected set; }

	public abstract string Name { get; set; }

	public abstract string Path { get; set; }

	public string ApiPath => AppConsts.ApiPath + '/' + Path.Trim('/');

	public string ApiPrefix => AppConsts.ApiPrefix + '/' + Path.Trim('/');

}
