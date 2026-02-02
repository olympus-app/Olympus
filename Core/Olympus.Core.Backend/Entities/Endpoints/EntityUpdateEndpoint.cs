namespace Olympus.Core.Backend.Entities;

public abstract class EntityUpdateEndpoint<TEntity, TUpdateRequest, TReadResponse>(IEntityService<TEntity> service) : EntityEndpoint<TEntity>.WithRequest<TUpdateRequest>.WithResponse<TReadResponse>(service) where TEntity : class, IEntity where TUpdateRequest : class, IEntityUpdateRequest where TReadResponse : class, IEntityReadResponse {

	public IEntityRequestMapper<TEntity, TUpdateRequest> RequestMapper { get; set; } = default!;

	public IEntityResponseMapper<TEntity, TReadResponse> ResponseMapper { get; set; } = default!;

	public override async Task<Void> HandleAsync(TUpdateRequest request, CancellationToken cancellationToken) {

		var entity = await Service.ReadAsync(request.Id, true, cancellationToken);

		if (entity is null) return await Send.NotFoundAsync(cancellationToken);

		if (ConflictCheck(entity)) return await Send.ConflictAsync(cancellationToken);

		RequestMapper.UpdateEntity(entity, request);

		entity = await Service.UpdateAsync(entity, cancellationToken);

		await Service.SaveChangesAsync(cancellationToken);

		entity.UpdatedBy = User.AsEntity();
		entity.CreatedBy ??= entity.UpdatedBy;

		var response = ResponseMapper.MapFromEntity(entity);

		return await Send.UpdatedAsync(response, cancellationToken);

	}

}
