namespace Olympus.Core.Backend.Entities;

public interface IEntityUpdateMapper<TEntity, TUpdateRequest, TReadResponse> : IRequestMapper<TUpdateRequest, TEntity>, IResponseMapper<TReadResponse, TEntity> where TEntity : class, IEntity where TUpdateRequest : class, IEntityUpdateRequest where TReadResponse : class, IEntityReadResponse { }
