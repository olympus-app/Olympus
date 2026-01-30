namespace Olympus.Core.Backend.Entities;

public abstract class EntityQueryEndpoint<TEntity, TQueryRequest, TQueryResponse>(IEntityService<TEntity> service) : EntityEndpoint<TEntity>.WithRequest<TQueryRequest>.WithResponse<EntityPageResult<TQueryResponse>>(service) where TEntity : class, IEntity where TQueryRequest : class, IEntityQueryRequest where TQueryResponse : class, IEntityQueryResponse {

	public IEntityResponseMapper<TEntity, TQueryResponse> ResponseMapper { get; set; } = default!;

	public override Task<Void> HandleAsync(TQueryRequest request, CancellationToken cancellationToken) {

		var query = Service.Query(false);

		var projection = ResponseMapper.ProjectFromEntity(query);

		return Send.PageAsync(projection, request, Web.ResponseCache.PrivateRevalidate, cancellationToken);

	}

}
