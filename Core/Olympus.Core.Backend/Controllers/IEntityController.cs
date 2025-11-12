using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;

namespace Olympus.Core.Backend.Controllers;

public interface IEntityController<TEntity, TModel, TMapper> where TEntity : class, IEntity where TModel : class, IModel where TMapper : class, IMapper<TEntity, TModel> {

	public Task<ActionResult<IQueryable<TModel>>> Get(ODataQueryOptions<TModel> options);

	public Task<ActionResult<TModel>> Get(Guid key, ODataQueryOptions<TModel> options, string? ifNoneMatch = null);

	public Task<ActionResult<TModel>> Post(TModel input, ODataQueryOptions<TModel> options);

	public Task<ActionResult<TModel>> Put(Guid key, TModel input, ODataQueryOptions<TModel> options, string? ifMatch = null, string? ifNoneMatch = null);

	public Task<ActionResult<TModel>> Patch(Guid key, Delta<TModel> input, ODataQueryOptions<TModel> options, string? ifMatch = null, string? ifNoneMatch = null);

	public Task<ActionResult> Delete(Guid key, ODataQueryOptions<TModel> options, string? ifMatch = null);

}
