using System.Net;

namespace Olympus.Core.Backend;

public class EntityNotFoundException<TEntity>(Guid? identifier, Exception? inner = null) : AppException(AppErrors.Keys.ResourceNotFound, typeof(TEntity).Name, identifier.ToString(), HttpStatusCode.NotFound, inner) where TEntity : IEntity { }
