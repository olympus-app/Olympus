namespace Olympus.Core.Backend.Entities;

public interface IEntityQueryConfiguration<TEntityQueryResponse> : IGridifyMapper<TEntityQueryResponse>, ISingletonService<IEntityQueryConfiguration<TEntityQueryResponse>> where TEntityQueryResponse : class, IEntityQueryResponse { }
