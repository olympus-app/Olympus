namespace Olympus.Core.Services;

public interface IService { }

public interface IService<TType> : IService where TType : class { }

public interface IServiceInheritor : IService { }
