namespace Olympus.Core.Services;

public interface ITransientService : IService { }

public interface ITransientService<TType> : IService<TType> where TType : class { }

public interface ITransientServiceInheritor : IServiceInheritor { }
