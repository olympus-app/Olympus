using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Olympus.Core.Backend;

[Consumes("application/json")]
[Produces("application/json; odata.metadata=minimal")]
[ProducesResponseType((int)HttpStatusCode.BadRequest)]
public abstract class EntityController<TEntity, TModel, TMapper>(IEntityService<TEntity> service) : ODataController, IEntityController<TEntity, TModel, TMapper> where TEntity : class, IEntity where TModel : class, IModel where TMapper : class, IMapper<TEntity, TModel> {

	private const AllowedQueryOptions CollectionQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Expand | AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.SkipToken | AllowedQueryOptions.Count | AllowedQueryOptions.Search | AllowedQueryOptions.Apply | AllowedQueryOptions.Compute;
	private const AllowedQueryOptions SingleItemQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Expand | AllowedQueryOptions.Compute;

	[ProducesResponseType((int)HttpStatusCode.OK)]
	[EnableQuery(AllowedQueryOptions = CollectionQueryOptions, MaxExpansionDepth = 5, MaxOrderByNodeCount = 10)]
	public virtual async Task<ActionResult<IQueryable<TModel>>> Get(ODataQueryOptions<TModel> options) {

		var query = await service.QueryAsync();

		var result = TMapper.ProjectFrom(query);

		return Ok(result);

	}

	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[EnableQuery(AllowedQueryOptions = SingleItemQueryOptions, MaxExpansionDepth = 5, MaxOrderByNodeCount = 10)]
	public virtual async Task<ActionResult<TModel>> Get([FromODataUri] Guid key, ODataQueryOptions<TModel> options) {

		var query = await service.QueryAsync(key);

		var result = TMapper.ProjectFrom(query);

		return Ok(SingleResult.Create(result));

	}

	[ProducesResponseType((int)HttpStatusCode.Created)]
	[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
	public virtual async Task<ActionResult<TModel>> Post([FromBody] TModel input, ODataQueryOptions<TModel> options) {

		if (!ModelState.IsValid) return BadRequest(ModelState);

		var entity = TMapper.MapFrom(input);

		entity = await service.CreateAsync(entity);

		var result = TMapper.MapFrom(entity);

		return Created(result);

	}

	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
	public virtual async Task<ActionResult<TModel>> Put([FromODataUri] Guid key, [FromBody] TModel input, ODataQueryOptions<TModel> options) {

		if (!ModelState.IsValid) return BadRequest(ModelState);

		var entity = await service.GetAsync(key);

		TMapper.MapTo(entity, input);

		// if (id != input.Id) throw new ArgumentException("O ID da rota não corresponde ao ID da entidade no corpo da requisição.");

		entity = await service.UpdateAsync(entity);

		var result = TMapper.MapFrom(entity);

		return Updated(result);

	}

	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
	public virtual async Task<ActionResult<TModel>> Patch([FromODataUri] Guid key, [FromBody] Delta<TModel> input, ODataQueryOptions<TModel> options) {

		if (!ModelState.IsValid) return BadRequest(ModelState);

		var entity = await service.GetAsync(key);

		var update = TMapper.MapFrom(entity);

		input.Patch(update);

		TMapper.MapTo(entity, update);

		// if (id != input.Id) throw new ArgumentException("O ID da rota não corresponde ao ID da entidade no corpo da requisição.");

		// var etag = options.IfMatch["Version"] as Guid?;

		entity = await service.UpdateAsync(entity);

		var result = TMapper.MapFrom(entity);

		return Updated(result);

	}

	[ProducesResponseType((int)HttpStatusCode.NoContent)]
	[ProducesResponseType((int)HttpStatusCode.NotFound)]
	[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
	public virtual async Task<ActionResult> Delete([FromODataUri] Guid key, ODataQueryOptions<TModel> options) {

		if (!ModelState.IsValid) return BadRequest(ModelState);

		var entity = await service.GetAsync(key);

		await service.DeleteAsync(entity);

		return NoContent();

	}

}
