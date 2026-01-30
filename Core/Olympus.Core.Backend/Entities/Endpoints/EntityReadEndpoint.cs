namespace Olympus.Core.Backend.Entities;

public abstract class EntityReadEndpoint<TEntity, TReadRequest, TReadResponse>(IEntityService<TEntity> service) : EntityEndpoint<TEntity>.WithRequest<TReadRequest>.WithResponse<TReadResponse>(service) where TEntity : class, IEntity where TReadRequest : class, IEntityReadRequest where TReadResponse : class, IEntityReadResponse {

	public IEntityResponseMapper<TEntity, TReadResponse> ResponseMapper { get; set; } = default!;

	public override async Task<Void> HandleAsync(TReadRequest request, CancellationToken cancellationToken) {

		var query = Service.Query(request.Id, false);

		var projection = ResponseMapper.ProjectFromEntity(query);

		var response = await projection.SingleOrDefaultAsync(cancellationToken);

		if (response is null) return await Send.NotFoundAsync(cancellationToken);

		return await Send.ItemAsync(response, Web.ResponseCache.PrivateRevalidate, cancellationToken);

	}

}
