namespace Olympus.Core.Services;

public interface IOptionsService : IService {

	public void Configure(IConfiguration configuration);

}

public interface IOptionsService<TType> : IService<TType> where TType : class {

	public void Configure(IConfiguration configuration);

}
