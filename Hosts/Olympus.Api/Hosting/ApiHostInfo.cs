namespace Olympus.Api.Hosting;

public abstract class ApiHostInfo : AppHostInfo {

	public abstract IEnumerable<Type> EndpointsTypes { get; init; }

	public abstract Action<ReflectionCache> EndpointsReflection { get; init; }

}
