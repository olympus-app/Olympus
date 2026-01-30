using System.Linq.Expressions;

namespace Olympus.Core.Backend.Entities;

public interface IEntityResponseMapper<TEntity, TResponse> : ISingletonService<IEntityResponseMapper<TEntity, TResponse>> where TEntity : class, IEntity where TResponse : class, IResponse {

	public TEntity MapToEntity(TResponse request);

	public TResponse MapFromEntity(TEntity entity);

	public Expression<Func<TEntity, TResponse>> ProjectFromEntity();

	public IQueryable<TResponse> ProjectFromEntity(IQueryable<TEntity> query);

}
