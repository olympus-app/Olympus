namespace Olympus.Core.Services;

public interface IScopedService : IService { }

public interface IScopedService<TType> : IService<TType> where TType : class { }

public interface IScopedServiceInheritor : IServiceInheritor { }
