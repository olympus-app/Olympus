using System.Linq.Expressions;

namespace Olympus.Core.Backend.Entities;

public interface IEntityRequestMapper<TEntity, TRequest> : ISingletonService<IEntityRequestMapper<TEntity, TRequest>> where TEntity : class, IEntity where TRequest : class, IRequest {

	public TEntity MapToEntity(TRequest request);

	public TRequest MapFromEntity(TEntity entity);

	public void UpdateEntity(TEntity entity, TRequest request);

	public Expression<Func<TEntity, TRequest>> ProjectFromEntity();

	public IQueryable<TRequest> ProjectFromEntity(IQueryable<TEntity> query);

}
