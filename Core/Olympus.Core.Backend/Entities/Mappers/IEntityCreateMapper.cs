namespace Olympus.Core.Backend.Entities;

public interface IEntityCreateMapper<TEntity, TCreateRequest, TReadResponse> : IRequestMapper<TCreateRequest, TEntity>, IResponseMapper<TReadResponse, TEntity> where TEntity : class, IEntity where TCreateRequest : class, IEntityCreateRequest where TReadResponse : class, IEntityReadResponse { }
