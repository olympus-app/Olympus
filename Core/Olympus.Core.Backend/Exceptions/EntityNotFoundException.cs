using System.Net;

namespace Olympus.Core.Backend.Exceptions;

public class EntityNotFoundException<TEntity>(Guid? identifier, Exception? inner = null) : AppBackendException(AppErrors.Keys.ResourceNotFound, typeof(TEntity).Name, identifier.ToString(), HttpStatusCode.NotFound, inner) where TEntity : IEntity { }
