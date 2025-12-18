namespace Olympus.Core.Backend.Entities;

public interface IEntityListMapper<TEntity, TListResponse> : IResponseMapper<IQueryable<TListResponse>, IQueryable<TEntity>> where TEntity : class, IEntity where TListResponse : class, IEntityListResponse { }
