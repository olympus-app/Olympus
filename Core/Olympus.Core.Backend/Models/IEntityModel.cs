using Asp.Versioning.OData;

namespace Olympus.Core.Backend.Models;

public interface IEntityModel : IModelConfiguration { }

public interface IEntityModel<TEntity, TModel> : IEntityModel where TEntity : class, IEntity where TModel : class, IModel { }
