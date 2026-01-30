namespace Olympus.Core.Services;

public interface ISingletonService : IService { }

public interface ISingletonService<TType> : IService<TType> where TType : class { }

public interface ISingletonServiceInheritor : IServiceInheritor { }
