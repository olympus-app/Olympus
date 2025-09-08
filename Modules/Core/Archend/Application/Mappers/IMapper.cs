namespace Olympus.Core.Archend;

public interface IMapper<TEntity, TModel> where TEntity : class, IEntity where TModel : class, IModel {

	public static abstract TEntity MapFrom(TModel model);

	public static abstract TModel MapFrom(TEntity entity);

	public static abstract void MapTo([MappingTarget] TEntity entity, TModel model);

	public static abstract void MapTo([MappingTarget] TModel model, TEntity entity);

	public static abstract IQueryable<TModel> ProjectFrom(IQueryable<TEntity> query);

	public static abstract IQueryable<TEntity> ProjectFrom(IQueryable<TModel> query);

}
