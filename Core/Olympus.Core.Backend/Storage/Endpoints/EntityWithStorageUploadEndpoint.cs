namespace Olympus.Core.Backend.Storage;

public abstract class EntityWithStorageUploadEndpoint<TEntityWithStorage, TStorageEntity, TEntityWithStorageUploadRequest>(IEntityWithStorageService<TEntityWithStorage, TStorageEntity> service) : EntityEndpoint<TEntityWithStorage>.WithRequest<TEntityWithStorageUploadRequest>(service) where TEntityWithStorage : class, IEntityWithStorage<TStorageEntity>, new() where TStorageEntity : class, IStorageEntity, new() where TEntityWithStorageUploadRequest : class, IEntityWithStorageUploadRequest {

	protected virtual TEntityWithStorage Initialize(TEntityWithStorageUploadRequest request, IFormFile file) {

		return new TEntityWithStorage() {
			Id = request.Id,
			File = new TStorageEntity() {
				Name = file.FileName,
				ContentType = file.ContentType,
				Bucket = StorageLocation.Private,
				Path = typeof(TStorageEntity).Name.Crop("Storage").Pluralize(),
				Size = file.Length,
			},
		};

	}

	public override async Task<Void> HandleAsync(TEntityWithStorageUploadRequest request, CancellationToken cancellationToken) {

		if (Files.Count == 0) return await Send.BadRequestAsync(ErrorsStrings.Values.NoFileSent, cancellationToken);

		var file = Files[0];

		var entity = await Service.ReadAsync(request.Id, false, cancellationToken);

		entity ??= Initialize(request, file);

		if (ConflictCheck(entity, request.ETag)) return await Send.ConflictAsync(cancellationToken);

		entity = await service.UploadAsync(entity, file.OpenReadStream(), cancellationToken);

		return await Send.UploadedAsync(entity.File, cancellationToken);

	}

}
