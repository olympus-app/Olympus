namespace Olympus.Core.Backend.Entities;

public interface IEntityQueryMapper<TEntity, TQueryResponse> : IResponseMapper<IQueryable<TQueryResponse>, IQueryable<TEntity>> where TEntity : class, IEntity where TQueryResponse : class, IEntityQueryResponse { }
