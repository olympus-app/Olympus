namespace Olympus.Core.Backend.Entities;

public abstract class EntityCreateEndpoint<TEntity, TCreateRequest, TReadResponse>(IEntityService<TEntity> service) : EntityEndpoint<TEntity>.WithRequest<TCreateRequest>.WithResponse<TReadResponse>(service) where TEntity : class, IEntity where TCreateRequest : class, IEntityCreateRequest where TReadResponse : class, IEntityReadResponse {

	public IEntityRequestMapper<TEntity, TCreateRequest> RequestMapper { get; set; } = default!;

	public IEntityResponseMapper<TEntity, TReadResponse> ResponseMapper { get; set; } = default!;

	public override async Task<Void> HandleAsync(TCreateRequest request, CancellationToken cancellationToken) {

		var entity = RequestMapper.MapToEntity(request);

		entity = await Service.CreateAsync(entity, cancellationToken);

		var response = ResponseMapper.MapFromEntity(entity);

		return await Send.CreatedAsync(response, cancellationToken);

	}

}
