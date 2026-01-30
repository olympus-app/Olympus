namespace Olympus.Core.Backend.Entities;

public abstract class EntityListEndpoint<TEntity, TListRequest, TListResponse>(IEntityService<TEntity> service) : EntityEndpoint<TEntity>.WithRequest<TListRequest>.WithResponse<EntityListResult<TListResponse>>(service) where TEntity : class, IEntity where TListRequest : class, IEntityListRequest where TListResponse : class, IEntityListResponse {

	public IEntityResponseMapper<TEntity, TListResponse> ResponseMapper { get; set; } = default!;

	public override Task<Void> HandleAsync(TListRequest request, CancellationToken cancellationToken) {

		var query = Service.Query(false).Cacheable(CacheExpirationMode.Absolute, 24.Hours());

		var projection = ResponseMapper.ProjectFromEntity(query);

		return Send.ListAsync(projection, Web.ResponseCache.From(CacheLocation.Private, CachePolicy.Revalidate, 24.Hours()), cancellationToken);

	}

}
