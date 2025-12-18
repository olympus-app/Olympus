namespace Olympus.Core.Backend.Entities;

public interface IEntityReadMapper<TEntity, TReadResponse> : IResponseMapper<IQueryable<TReadResponse>, IQueryable<TEntity>> where TEntity : class, IEntity where TReadResponse : class, IEntityReadResponse { }
