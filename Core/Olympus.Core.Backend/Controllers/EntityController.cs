using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Olympus.Core.Backend.Controllers;

[Consumes(ContentTypes.Json)]
[Produces(ContentTypes.ODataMetadataMinimal, ContentTypes.ODataMetadataNone)]
public abstract class EntityController<TEntity, TModel, TMapper>(IEntityService<TEntity> service) : ODataController, IEntityController<TEntity, TModel, TMapper> where TEntity : class, IEntity where TModel : class, IModel where TMapper : class, IMapper<TEntity, TModel> {

	[ODataQuery(ODataQueryType.Page)]
	[ProducesResponse(HttpStatusCode.OK)]
	[ProducesResponse(HttpStatusCode.NotFound)]
	public virtual async Task<ActionResult<IQueryable<TModel>>> Get([FromServices] ODataQueryOptions<TModel> options) {

		var query = await service.QueryAsync();

		var result = TMapper.ProjectFrom(query);

		return Ok(result);

	}

	[ODataQuery(ODataQueryType.Item)]
	[ProducesResponse(HttpStatusCode.OK)]
	[ProducesResponse(HttpStatusCode.NotFound)]
	[ProducesResponse(HttpStatusCode.NotModified)]
	[ProducesResponse(HttpStatusCode.BadRequest)]
	public virtual async Task<ActionResult<TModel>> Get([FromRoute] Guid key, [FromServices] ODataQueryOptions<TModel> options, [FromHeader(Name = "if-none-match")] string? ifNoneMatch = null) {

		if (CheckIfNoneMatch(key, options) is ActionResult check) return check;

		var query = await service.QueryAsync(key);

		var result = TMapper.ProjectFrom(query);

		return Ok(SingleResult.Create(result));

	}

	[ODataQuery(ODataQueryType.None)]
	[ProducesResponse(HttpStatusCode.Created)]
	[ProducesResponse(HttpStatusCode.BadRequest)]
	[ProducesResponse(HttpStatusCode.UnprocessableContent)]
	public virtual async Task<ActionResult<TModel>> Post([FromBody] TModel input, [FromServices] ODataQueryOptions<TModel> options) {

		if (CheckInput(input) is ActionResult check) return check;

		var entity = TMapper.MapFrom(input);

		entity = await service.CreateAsync(entity);

		var result = TMapper.MapFrom(entity);

		return Created(result);

	}

	[ODataQuery(ODataQueryType.None)]
	[ProducesResponse(HttpStatusCode.OK)]
	[ProducesResponse(HttpStatusCode.NotFound)]
	[ProducesResponse(HttpStatusCode.BadRequest)]
	[ProducesResponse(HttpStatusCode.UnprocessableContent)]
	[ProducesResponse(HttpStatusCode.PreconditionRequired)]
	[ProducesResponse(HttpStatusCode.PreconditionFailed)]
	public virtual async Task<ActionResult<TModel>> Put([FromRoute] Guid key, [FromBody] TModel input, [FromServices] ODataQueryOptions<TModel> options, [FromHeader(Name = "if-match")] string? ifMatch = null, [FromHeader(Name = "if-none-match")] string? ifNoneMatch = null) {

		if (CheckInput(input) is ActionResult check1) return check1;

		if (CheckIdentifier(key, input) is ActionResult check2) return check2;

		var entity = await service.GetAsync(key);

		if (CheckIfMatch(entity, options) is ActionResult check3) return check3;

		if (CheckIfNoneMatch(entity, options) is ActionResult check4) return check4;

		TMapper.MapTo(entity, input);

		entity = await service.UpdateAsync(entity);

		var result = TMapper.MapFrom(entity);

		return Updated(result);

	}

	[ODataQuery(ODataQueryType.None)]
	[ProducesResponse(HttpStatusCode.OK)]
	[ProducesResponse(HttpStatusCode.NotFound)]
	[ProducesResponse(HttpStatusCode.BadRequest)]
	[ProducesResponse(HttpStatusCode.UnprocessableContent)]
	[ProducesResponse(HttpStatusCode.PreconditionRequired)]
	[ProducesResponse(HttpStatusCode.PreconditionFailed)]
	public virtual async Task<ActionResult<TModel>> Patch([FromRoute] Guid key, [FromBody] Delta<TModel> input, [FromServices] ODataQueryOptions<TModel> options, [FromHeader(Name = "if-match")] string? ifMatch = null, [FromHeader(Name = "if-none-match")] string? ifNoneMatch = null) {

		if (CheckInput(input) is ActionResult check1) return check1;

		if (CheckIdentifier(key, input) is ActionResult check2) return check2;

		var entity = await service.GetAsync(key);

		if (CheckIfMatch(entity, options) is ActionResult check3) return check3;

		var update = TMapper.MapFrom(entity);

		input.Patch(update);

		TMapper.MapTo(entity, update);

		entity = await service.UpdateAsync(entity);

		var result = TMapper.MapFrom(entity);

		return Updated(result);

	}

	[ODataQuery(ODataQueryType.None)]
	[ProducesResponse(HttpStatusCode.NotFound)]
	[ProducesResponse(HttpStatusCode.NoContent)]
	[ProducesResponse(HttpStatusCode.BadRequest)]
	[ProducesResponse(HttpStatusCode.PreconditionRequired)]
	[ProducesResponse(HttpStatusCode.PreconditionFailed)]
	public virtual async Task<ActionResult> Delete([FromRoute] Guid key, [FromServices] ODataQueryOptions<TModel> options, [FromHeader(Name = "if-match")] string? ifMatch = null) {

		var entity = await service.GetAsync(key);

		if (CheckIfMatch(entity, options) is ActionResult check) return check;

		await service.DeleteAsync(entity);

		return NoContent();

	}

	protected ActionResult? CheckInput(object? input) {

		if (input is null) return BadRequest();

		if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

		return null;

	}

	protected ActionResult? CheckIdentifier(Guid key, object input) {

		var id = Guid.Empty;

		if (input is TModel model) id = model.Id;

		if (input is Delta<TModel> delta) if (delta.TryGetPropertyValue("Id", out var idValue) && idValue is Guid guidValue) id = guidValue;

		if (key != id) return BadRequest("Route key does not match entity identifier in the request body.");

		return null;

	}

	protected ActionResult? CheckIfMatch(TEntity entity, ODataQueryOptions<TModel> options) {

		var ifMatch = options.IfMatch;

		if (ifMatch is null) return StatusCode(HttpStatusCode.PreconditionRequired.Value);

		if (ifMatch.IsAny) return null;

		if (!ifMatch.IsWellFormed) return BadRequest("If-Match header content is malformed.");

		var etag = ifMatch["version"];

		if (etag is Guid value && value == entity.Version) return null;

		return StatusCode(HttpStatusCode.PreconditionFailed.Value, TMapper.MapFrom(entity));

	}

	protected ActionResult? CheckIfNoneMatch(TEntity entity, ODataQueryOptions<TModel> options) {

		var ifNoneMatch = options.IfNoneMatch;

		if (ifNoneMatch is null) return null;

		if (ifNoneMatch.IsAny) return null;

		if (!ifNoneMatch.IsWellFormed) return BadRequest("If-None-Match header content is malformed.");

		var etag = ifNoneMatch["version"];

		if (etag is Guid value && value == entity.Id) return StatusCode(HttpStatusCode.NotModified.Value);

		return null;

	}

	protected ActionResult? CheckIfNoneMatch(Guid key, ODataQueryOptions<TModel> options) {

		var ifNoneMatch = options.IfNoneMatch;

		if (ifNoneMatch is null) return null;

		if (ifNoneMatch.IsAny) return null;

		if (!ifNoneMatch.IsWellFormed) return BadRequest("If-None-Match header content is malformed.");

		var version = service.QueryAsync(key).Result.Select(x => x.Version).SingleOrDefault();

		if (version == Guid.Empty) return NotFound();

		var etag = ifNoneMatch["version"];

		if (etag is Guid value && value == version) return StatusCode(HttpStatusCode.NotModified.Value);

		return null;

	}

}
